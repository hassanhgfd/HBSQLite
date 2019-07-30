using System.Collections.Generic;

namespace HBSQLite
{
    /// <summary>
    /// provide method methods for read all the Data in the DataBase
    /// </summary>
    public interface IQueryReadAllData
    {
        /// <summary>
        /// make query for Read All the Data From the Table
        /// </summary>
        /// <returns>query for Read All the Data From the Table</returns>
        string Read();

        /// <summary>
        /// make query for Read All the Data From the Table
        /// </summary>
        /// <param name="tableColumn">the column that will be returned </param>
        /// <returns>query for Read All the Data From the Table</returns>
        string Read(ref SQLiteTableColumn tableColumn);

        /// <summary>
        /// make query for Read one column From the Table
        /// </summary>
        /// <param name="tableColumn">the column that will be returned </param>
        /// <param name="orderByWay"></param>
        /// <returns>query for Read All the Data From the Table</returns>
        string Read(ref SQLiteTableColumn tableColumn, OrderByWay orderByWay);

        /// <summary>
        /// make query for Read more than one column From the Table
        /// </summary>
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// <param name="tableColumns">the columns that will be returned </param>
        /// <returns>query for Read All the Data From the Table</returns>
        string Read(IEnumerable<SQLiteTableColumn> tableColumns);

        /// <summary>
        /// make query for Read more than one column From the Table
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns">the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy"/> for order the result </param>
        /// <returns>query for Read All the Data From the Table</returns>
        string Read(IEnumerable<SQLiteTableColumn> tableColumns, OrderBy orderBy);
    }
}