using System.Collections.Generic;

namespace HBSQLite
{
    /// <summary>
    /// interface for <see cref="OrderBy"/>
    /// </summary>
    public interface IOrderBy
    {
        /// <summary>
        /// the columns that will be used for sorting
        /// </summary>
        List<SQLiteTableColumn> Columns { get; }

        /// <summary>
        /// the way to oder 
        /// </summary>
        OrderByWay OrderByWay { get; set; }

        /// <summary>
        /// add new column to the list
        /// </summary>
        /// <param name="columns">the column that will be used for sorting</param>
        void Add(SQLiteTableColumn columns);

        /// <summary>
        /// convert this object to string that can used in query
        /// </summary>
        /// <returns>string can used in query</returns>
        string ToString();
    }
}