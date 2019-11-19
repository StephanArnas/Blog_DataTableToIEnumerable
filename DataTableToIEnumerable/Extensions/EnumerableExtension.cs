using DataTableToIEnumerable.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataTableToIEnumerable.Extensions
{
    public static class EnumerableExtension
    {
        #region ----- PUBLIC METHODS --------------------------------------------------

        public static DataTable ToDataTable<T>(this IEnumerable<T> source) where T : new()
        {
            return ToDataTable(source, typeof(T));
        }

        public static DataTable ToDataTable(this IEnumerable source, Type type)
        {
            if (source == null) return null;

            // Get the attribut name on the class to define the table name.
            TableFieldNameAttribute attribut = type
                .GetCustomAttributes(typeof(TableFieldNameAttribute), true)
                .FirstOrDefault() as TableFieldNameAttribute;
            var dataTable = new DataTable(attribut != null ? attribut.Name : string.Empty);
            var properties = type.GetProperties();

            // Create the columns.
            foreach (var property in properties)
            {
                attribut = property
                    .GetCustomAttributes(typeof(TableFieldNameAttribute), true)
                    .FirstOrDefault() as TableFieldNameAttribute;
                if (attribut == null) continue;
                if (property.PropertyType == typeof(bool))
                {
                    dataTable.Columns.Add(attribut.Name, typeof(bool));
                }
                else dataTable.Columns.Add(attribut.Name);
            }

            // Insert the rows.
            object[] values;
            int valuesIndex = 0;
            foreach (var item in source)
            {
                // Create a temporary object which represent one row.
                values = new object[dataTable.Columns.Count];
                for (int i = 0; i < properties.Length; i++)
                {
                    if (properties[i].GetCustomAttributes(typeof(TableFieldNameAttribute), true).FirstOrDefault() != null)
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(properties[i].PropertyType) 
                            && properties[i].PropertyType.IsNonStringEnumerable())
                        {
                            // TODO : We have an IEnumerable object and we need to do some recursivity here.
                            // I just started the code but it's not finished and it's not working. 
                            // If you want to help, please make a pull request (I will reference you in my article also).

                            // The current item is an Enumerable source, so we need a custom process.
                            // Store the current IEnumerable property into a variable.
                            var propertyEnumerable = properties[i].GetValue(item, null);
                            // Get the type of the IEnumerable.
                            var typeOfEnumerable = propertyEnumerable.GetType().GetGenericArguments()[0];
                            // Use some recursivity to transform the IEnumerable to a DataTable.
                            values[valuesIndex] = ((IEnumerable)propertyEnumerable).ToDataTable(typeOfEnumerable);
                        }
                        else
                        {
                            // Fulfill the temporary object with all the values from the item in the enumerable.
                            values[valuesIndex] = properties[i].GetValue(item, null);
                        }
                        // Custom index because we want to avoid the properties which don't have the attribut.
                        valuesIndex++;
                    }
                }
                // Add a new row with the temporary object
                dataTable.Rows.Add(values);
                valuesIndex = 0;
            }

            // When AcceptChanges is called, any DataRow object still in edit mode successfully ends its edits
            dataTable.AcceptChanges();
            return dataTable;
        }

        public static ICollection<T> UpdatePropertyValue<T>(this ICollection<T> source
            , Expression<Func<T, object>> expression
            , object value) where T : new()
        {
            if (source == null) return null;
            if (expression == null) return source;

            var propertyName = expression.Body.GetMemberName();
            foreach (var item in source)
            {
                var propertyInfo = item.GetType().GetProperty(propertyName);
                propertyInfo.SetValue(item, value);
            }
            return source;
        }

        #endregion
    }
}
