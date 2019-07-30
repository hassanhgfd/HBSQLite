using System.Collections.Generic;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// interface for <see cref="SQLiteDataBase"/>
    /// </summary>
    public interface ISQLiteDataBase
    {
        /// <summary>
        /// List Of <see cref="SQLiteTable"/> contain all DataBase Tables
        /// </summary>
        List<SQLiteTable> Tables { get; }

        /// <summary>
        /// the DataSource For this ReaderIndex
        /// </summary>
        string DataSource { get; }

        /// <summary>
        /// Get the Column by it name
        /// </summary>
        /// <param name="tableName">the name of Table</param>
        /// <returns>the Table object</returns>
        /// <exception cref="SQLiteTableNotFoundException"></exception>
        SQLiteTable this[string tableName] { get; }

        /// <summary>
        /// Get the Column by it name
        /// </summary>
        /// <param name="tableName"><see cref="SQLiteTable"/> table object</param>
        /// <returns>the Table object</returns>
        /// <exception cref="SQLiteTableNotFoundException"></exception>
        SQLiteTable this[SQLiteTable tableName] { get; }
    }
}