using System;
using System.Collections.Generic;
using System.Text;

namespace DataTableToIEnumerable.Extensions
{
    public static class TypeExtension
    {
        #region ----- Public Methods --------------------------------------------------

        public static bool IsNonStringEnumerable(this Type type)
        {
            if (type == null || type == typeof(string))
                return false;
            return typeof(System.Collections.IEnumerable).IsAssignableFrom(type);
        }

        #endregion
    }
}
