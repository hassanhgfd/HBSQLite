using System;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// interface for <see cref="ObjectReader"/>
    /// </summary>
    public interface IObjectReader
    {
        /// <summary>
        /// the Table that contain the column that written in the object
        /// </summary>
        SQLiteTable Table { get; }

        /// <summary>
        /// read the object properties and return them as <see cref="Tuple{T1,T2}"/>
        /// where the T1 is name of property and the T2 is the value of property that inside that properties you've made
        /// and both T1 and T2 are arrays
        /// </summary>
        /// <typeparam name="T">class or struct contain properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="object">the object that has all it properties of type <see cref="ColumnValue{T}"/></param>
        /// <returns></returns>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        (string[] Names, string[] Values) GetPropertiesNamesValues<T>(T @object);

        /// <summary>
        /// ReadObjects the object properties and they should <see cref="ColumnValue{T}"/>
        /// and convert them to string 
        /// </summary>
        /// <typeparam name="T">class or struct contain properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="object">the object that has all it properties of type <see cref="ColumnValue{T}"/></param>
        /// <param name="isSet">is object must be isSetColumnToValue</param>
        /// <returns></returns>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        string[] GetPropertiesColumnValues<T>(T @object, bool isSet);
    }
}