using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using FastMember;
using uFramework.Common.Extensions;

namespace uFramework.IO.Office
{
    public class Excel : IDisposable
    {
        private XLWorkbook _workbook;

        public Excel()
        {
            try
            {
                this._workbook =
                    new XLWorkbook(XLEventTracking.Disabled);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Excel(string file)
        {
            try
            {
                this._workbook =
                    new XLWorkbook(file, XLEventTracking.Disabled);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Excel(Stream stream)
        {
            try
            {
                this._workbook =
                    new XLWorkbook(stream, XLEventTracking.Disabled);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Load<T>(IEnumerable<T> source)
            where T : class, new()
        {
            var worksheet =
                this._workbook.AddWorksheet(typeof(T).Name);

            var props =
                GetExportableProperties(typeof(T));

            var table =
                new DataTable(typeof(T).Name);

            using (var reader = ObjectReader.Create(source, props.Select(p => p.Name).ToArray()))
            {
                table.Load(reader);
            }

            worksheet.FirstRow().FirstCell().InsertTable(table, typeof(T).Name);
        }

        public IEnumerable<T> ToList<T>(int worksheet)
            where T : class, new()
        {
            return ToList<T>(this._workbook.Worksheet(worksheet));
        }

        public IEnumerable<T> ToList<T>(string worksheet)
            where T : class, new()
        {
            return ToList<T>(this._workbook.Worksheet(worksheet));
        }

        private IEnumerable<T> ToList<T>(IXLWorksheet worksheet)
            where T : class, new()
        {
            var props =
                GetImportableProperties(typeof(T));

            var fields =
                worksheet.Table(typeof(T).Name).Fields;

            var data =
                worksheet.Table(typeof(T).Name).DataRange;

            if (fields.All(f => props.Select(p => p.Name).Contains(f.Name)) == false)
                throw new MissingFieldException();

            var export =
                new List<T>();

            foreach (var r in data.Rows())
            {
                var e =
                    new T();

                foreach (var p in props)
                {
                    Type t =
                        Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;

                    object value =
                        (r.Field(p.Name).Value == null || r.Field(p.Name).Value.ToString().IsEmpty()) ? null : Convert.ChangeType(r.Field(p.Name).Value, t);

                    p.SetValue(e, value, null);
                }

                export.Add(e);
            }

            return export;
        }

        public void Save()
        {
            this._workbook.Save();
        }

        public void SaveAs(Stream stream)
        {
            this._workbook.SaveAs(stream);
        }

        public void SaveAs(string file)
        {
            this._workbook.SaveAs(file);
        }

        public void Dispose()
        {
            this._workbook = null;
        }

        #region Private Members

        private IEnumerable<PropertyInfo> GetExportableProperties(Type type)
        {
            return
                type.GetPropertiesByAttribute<uFramework.IO.Attributes.ExportAttribute>();
        }

        private IEnumerable<PropertyInfo> GetImportableProperties(Type type)
        {
            return
                type.GetPropertiesByAttribute<uFramework.IO.Attributes.ImportAttribute>();
        }

        #endregion
    }
}
