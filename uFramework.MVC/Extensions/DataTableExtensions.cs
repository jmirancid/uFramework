using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using uFramework.Common.Extensions;

namespace uFramework.MVC.Extensions
{
    public static class DataTableExtensions
    {
        public static MvcHtmlString DataTable<TModel, TProperty>
            (this HtmlHelper<IEnumerable<TModel>> htmlHelper, params Expression<Func<TModel, TProperty>>[] columns)
            where TModel : class
            where TProperty : class
        {
            return DataTable(htmlHelper, HtmlHelper.GenerateIdFromName(typeof(TModel).Name), columns);
        }

        public static MvcHtmlString DataTable<TModel, TProperty>
            (this HtmlHelper<IEnumerable<TModel>> htmlHelper, string name, params Expression<Func<TModel, TProperty>>[] columns)
            where TModel : class
            where TProperty : class
        {
            return DataTable(htmlHelper, name, null, columns);
        }

        public static MvcHtmlString DataTable<TModel, TProperty>
            (this HtmlHelper<IEnumerable<TModel>> htmlHelper, string name, object options, params Expression<Func<TModel, TProperty>>[] columns)
            where TModel : class
            where TProperty : class
        {
            return DataTable(htmlHelper, name, HtmlHelper.AnonymousObjectToHtmlAttributes(options), columns);
        }

        public static MvcHtmlString DataTable<TModel, TProperty>
            (this HtmlHelper<IEnumerable<TModel>> htmlHelper, string name, IDictionary<string, object> options, params Expression<Func<TModel, TProperty>>[] columns)
            where TModel : class
            where TProperty : class
        {
            if (columns.Count() < 0)
                return null;

            var table = new TagBuilder("table");
            table.Attributes["id"] = name;
            table.Attributes["border"] = "2";

            var thead = new TagBuilder("thead");
            var thead_tr = new TagBuilder("tr");
            foreach (var col in columns)
            {
                var th = new TagBuilder("th");
                th.SetInnerText(ModelMetadata.FromLambdaExpression<TModel, TProperty>(col, new ViewDataDictionary<TModel>()).DisplayName);

                thead_tr.InnerHtml += th.ToString();
            }
            thead.InnerHtml += thead_tr.ToString();

            var tbody = new TagBuilder("tbody");
            foreach (var col in columns)
            {
                var tbody_tr = new TagBuilder("tr");
                foreach (var item in htmlHelper.ViewData.Model)
                {
                    var td = new TagBuilder("td");
                    if (col is Expression<Func<TModel, TProperty>>)
                        td.InnerHtml = col.GetValueFrom(item).ToString();
                    else
                        td.InnerHtml = "Custom";

                    tbody_tr.InnerHtml += td.ToString();
                }
                tbody.InnerHtml += tbody_tr.ToString();
            }

            //var tfoot = new TagBuilder("tfoot");

            table.InnerHtml += thead.ToString();
            table.InnerHtml += tbody.ToString();
            //table.InnerHtml += tfoot.ToString();

            var script = new TagBuilder("script");
            script.Attributes["type"] = "text/javascript";

            var settings =
                (options == null ? string.Empty : (new JavaScriptSerializer()).Serialize(options.ToDictionary(o => o.Key.ToString(), o => o.Value.ToString())));

            script.InnerHtml = @"
                $(function() {
                    $('#" + name + @"').dataTable(" + settings + @");
                });";

            return MvcHtmlString.Create(table.ToString() + script.ToString());
        }
    }
}
