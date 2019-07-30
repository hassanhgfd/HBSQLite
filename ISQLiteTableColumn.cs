using System;
using System.ComponentModel;

namespace HBSQLite
{
    /// <summary>
    /// the <see cref="SQLiteTableColumn"/> Interface
    /// </summary>
    public interface ISQLiteTableColumn
    {
        /// <summary>
        /// the name of this column
        /// </summary>
        string Name { get; }

        /// <summary>
        /// convert this object to string by returning <see cref="SQLiteTableColumn.Name"/>
        /// </summary>
        /// <returns>return <see cref="SQLiteTableColumn.Name"/></returns>
        string ToString();
    }
}