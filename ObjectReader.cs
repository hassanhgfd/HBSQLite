using System;
using System.Collections.Generic;
using System.Reflection;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// provide methods that read the object convert it to string ready made for query
    /// </summary>
    public class ObjectReader : IObjectReader
    {
        /// <summary>
        /// the Table that contain the column that written in the object
        /// </summary>
        public SQLiteTable Table { get; }

        /// <summary>
        /// initialize new instance of <see cref="ObjectReader"/> withe Table
        /// </summary>
        /// <param name="table">the Table That Contain the Columns that comes with the object</param>
        public ObjectReader(SQLiteTable table) => Table = table;

        /// <summary>
        /// read the object properties and return them as <see cref="Tuple{T1, T2}"/>
        /// where the T1 is name of property and the T2 is the value of property that inside that properties you've made
        /// and both T1 and T2 are arrays
        /// </summary>
        /// <typeparam name="T">class or struct contain properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="object">the object that has all it properties of type <see cref="ColumnValue{T}"/></param>
        /// <returns></returns>
        /// <exception cref="SQLitePropertyNameNotAsColumnNameException"></exception>
        public (string[] Names, string[] Values) GetPropertiesNamesValues<T>(T @object)
        {
            var properties = typeof(T).GetProperties();
            var columnName = new List<string>();
            var columnValue = new List<string>();

            foreach (var property in properties)
            {
                var value = property.GetValue(@object);
                if (value == null) continue;
                CheckProperty(property);
                columnValue.Add(GetValue(@object, value, property, value.GetType().GetProperty("Value"),true, true));
                columnName.Add(Table[GetPropertyName(property)]);
            }

            return (columnName.ToArray(), columnValue.ToArray());
        }

        /// <summary>
        /// ReadObjects the object properties and they should <see cref="ColumnValue{T}"/>
        /// and convert them to string 
        /// </summary>
        /// <typeparam name="T">class or struct contain properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="object">the object that has all it properties of type <see cref="ColumnValue{T}"/></param>
        /// <param name="isSet">is object must be isSetColumnToValue</param>
        /// <returns></returns>
        /// <exception cref="SQLitePropertyNameNotAsColumnNameException"></exception>
        public string[] GetPropertiesColumnValues<T>(T @object, bool isSet)
        {
            var objectType = typeof(T);
            var properties = objectType.GetProperties();

            var list = new List<string>();
            foreach (var property in properties)
            {
                var value = property.GetValue(@object);
                if (value == null) continue;
                CheckProperty(property);
                list.Add(GetValue(@object, value, property, value.GetType().GetProperty("Value"), isSet, false));
            }

            return list.ToArray();
        }

        private void CheckProperty(PropertyInfo property)
        {
            try
            {
                _ = Table[GetPropertyName(property)];
            }
            catch (SQLiteTableColumnNotFoundException)
            {
                throw new SQLitePropertyNameNotAsColumnNameException(GetPropertyName(property), Table);
            }
        }

        private string GetValue<T>(T @object, object value, PropertyInfo property, PropertyInfo valueProperty, bool isSetColumnToValue, bool isGetColumnValue)
        {
            value = SetTableColumnName(value, property);
            if (isSetColumnToValue) value = SetColumnToValue(value);
            property.SetValue(@object, value);

            return isGetColumnValue? GetColumnValue(valueProperty, value) : value.ToString();
        }

        private object SetColumnToValue(object value)
        {
            value.GetType().GetProperty(nameof(ColumnToValue))
                ?.SetValue(value, ColumnToValue.Equal);
            return value;
        }

        private string GetPropertyName(PropertyInfo property) => property.Name.Split('_')[0];

        private object SetTableColumnName(object value, PropertyInfo property)
        {
            value.GetType().GetProperty("Name")
                ?.SetValue(value, GetPropertyName(property));
            return value;
        }

        private string GetColumnValue<T>(PropertyInfo columnPropertyInfo, T @object)
        {
            return columnPropertyInfo.PropertyType == typeof(string)
                ? $"'{columnPropertyInfo.GetValue(@object)}'"
                : columnPropertyInfo.GetValue(@object).ToString();
        }
    }
}