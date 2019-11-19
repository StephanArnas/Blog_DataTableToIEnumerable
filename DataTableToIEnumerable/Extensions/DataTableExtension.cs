using DataTableToIEnumerable.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DataTableToIEnumerable.Extensions
{
    public static class DataTableExtension
    {
        #region ----- Public Methods --------------------------------------------------

        public static ICollection<T> ToEnumerable<T>(this DataTable source) where T : new()
        {
            if (source == null) return null;

            // Init objects
            List<T> items = new List<T>();
            TableFieldNameAttribute attribut = null;
            var properties = typeof(T).GetProperties();

            T item;
            foreach (DataRow row in source.Rows)
            {
                // Create a new instance of the desired object.
                item = (T)Activator.CreateInstance(typeof(T));
                foreach (var property in properties)
                {
                    // Get the attribute.
                    attribut = property
                        .GetCustomAttributes(typeof(TableFieldNameAttribute), true)
                        .FirstOrDefault() as TableFieldNameAttribute;

                    // If there is no attribut, we need to skip the property.
                    if (attribut == null) continue;

                    if (source.Columns.Contains(attribut.Name))
                    {
                        if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) 
                            && property.PropertyType.IsNonStringEnumerable())
                        {
                            // TODO : Handle IEnumerable object. 
                            // If you want to help, please make a pull request (I will reference you in my article also). :)
                        }
                        else
                        {
                            // Set the desired object with the value from the row.
                            if (property.PropertyType == typeof(decimal) 
                                && row[attribut.Name].ToString() == string.Empty)
                            {
                                property.SetValue(item, Convert.ChangeType(0, property.PropertyType), null);
                            }
                            else
                            {
                                property.SetValue(item, Convert.ChangeType(row[attribut.Name], property.PropertyType), null);
                            }
                        }
                    }
                }
                items.Add(item);
            }

            return items;
        }

        public static string ToXml(this DataSet source)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (TextWriter streamWriter = new StreamWriter(memoryStream))
                {
                    var xmlSerializer = new XmlSerializer(typeof(DataSet));
                    xmlSerializer.Serialize(streamWriter, source);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
        }

        #endregion
    }
}
