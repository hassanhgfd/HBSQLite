using System.Collections.Generic;
using System.Data;

namespace HBSQLite
{
    /// <summary>
    /// provide method methods for read all the Data in the DataBase
    /// </summary>
    public interface IReadAllData
    {
        /// <summary>
        /// Read All the Data From the Table And Return them <see cref="DataTable"/>
        /// </summary>
        /// <returns></returns>
        DataTable ReadTable();

        /// <summary>
        /// Read one column From the Table And Return them <see cref="DataTable"/>
        /// </summary>
        /// <param name="tableColumn"> the column that will be returned </param>
        /// <returns></returns>
        DataTable ReadTable(SQLiteTableColumn tableColumn);

        /// <summary>
        /// Read one column From the Table And Return them <see cref="DataTable"/>
        /// </summary>
        /// <param name="tableColumn">the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        DataTable ReadTable(SQLiteTableColumn tableColumn, OrderByWay orderByWay);

        /// <summary>
        /// Read more than one column From the Table And Return them <see cref="DataTable"/>
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns">the columns that will be returned </param>
        /// <returns></returns>
        DataTable ReadTable(IEnumerable<SQLiteTableColumn> tableColumns);

        /// <summary>
        /// Read more than one column From the Table And Return them <see cref="DataTable"/>
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        DataTable ReadTable(IEnumerable<SQLiteTableColumn> tableColumns, OrderBy orderBy);

        /// <summary>
        /// Read All the Data From the Table And Return them array of objects
        /// </summary>
        /// <returns></returns>
        object[][] Read();

        /// <summary>
        /// Read one column From the Table And Return them array of objects
        /// </summary>
        /// <param name="tableColumn"> the column that will be returned </param>
        /// <returns></returns>
        object[] Read(SQLiteTableColumn tableColumn);

        /// <summary>
        /// Read one column From the Table And Return them array of objects
        /// </summary>
        /// <param name="tableColumn"> the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        object[] Read(SQLiteTableColumn tableColumn, OrderByWay orderByWay);

        /// <summary>
        /// Read more than one column From the Table And Return them array of objects
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// <param name="tableColumns"> the columns that will be returned </param>
        /// </summary>
        /// <returns></returns>
        object[][] Read(IEnumerable<SQLiteTableColumn> tableColumns);

        /// <summary>
        /// Read more than one column From the Table And Return them array of objects
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy"/> for order the result </param>
        /// <returns>array of objects</returns>
        object[][] Read(IEnumerable<SQLiteTableColumn> tableColumns, OrderBy orderBy);
    }
}