using System;
using System.Collections.Generic;
using System.Text;

namespace DataTableToIEnumerable.Attributes
{
    public class TableFieldNameAttribute : AttributeBase
    {
        #region ----- CONSTRUCTOR -----------------------------------------------------

        public TableFieldNameAttribute(string name) : base(name) { }

        #endregion
    }
}
