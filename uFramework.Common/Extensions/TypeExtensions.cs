using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace uFramework.Common.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetPropertiesByAttribute<T>(this Type source)
            where T : class
        {
            var props =
                new List<PropertyInfo>();

            props.AddRange(
                source.GetProperties().Where(
                    p => System.Attribute.IsDefined(p, typeof(T))));

            //check for meta data class.
            MetadataTypeAttribute[] metaAttr =
                (MetadataTypeAttribute[])source.GetCustomAttributes(typeof(MetadataTypeAttribute), true);

            if (metaAttr.Length > 0)
            {
                foreach (MetadataTypeAttribute attr in metaAttr)
                {
                    var metadataType =
                        attr.MetadataClassType;

                    var metadataProps =
                        metadataType.GetProperties().Where(
                            p => System.Attribute.IsDefined(p, typeof(T)));

                    props.AddRange(
                        metadataProps.Select(m => source.GetProperty(m.Name)));
                }
            }

            return props;
        }
    }
}
