using System;
using System.Collections.Generic;
using System.Text;

namespace DataTableToIEnumerable.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class AttributeBase : Attribute
    {
        #region ----- ATTRIBUTES ------------------------------------------------------

        private string _name = string.Empty;

        #endregion

        #region ----- CONSTRUCTOR -----------------------------------------------------

        public AttributeBase(string name)
        {
            _name = name;
        }

        #endregion

        #region ----- PROPERTIES ------------------------------------------------------

        public virtual string Name
        {
            get { return _name; }
        }

        #endregion
    }
}
