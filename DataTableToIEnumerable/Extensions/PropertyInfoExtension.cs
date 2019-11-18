using System;
using System.Collections.Generic;
using System.Text;

namespace DataTableToIEnumerable.Extensions
{
    public static class PropertyInfoExtension
    {
        #region ----- Public Methods --------------------------------------------------

        public static bool IsNonStringEnumerable(this object instance)
        {
            return instance != null && instance.GetType().IsNonStringEnumerable();
        }

        public static bool IsNonStringEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
                return false;
            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        public static string Iterate(this object property)
        {
            var source = property as System.Collections.IEnumerable;
            var result = string.Empty;
            if (source != null)
            {
                bool first = true;
                foreach (var item in source)
                {
                    if (!first)
                    {
                        result += ",";
                    }
                    result += item.ToString();
                    first = false;
                }
            }
            return result;
        }

        #endregion
    }
}
