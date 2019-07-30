using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace HBSQLite
{
    /// <summary>
    /// proved methods to discover the <see cref="SQLiteDataBase"/> and <see cref="SQLiteTable"/>
    /// </summary>
    public static class Discover
    {
        /// <summary>
        /// DisCover all the DataBase Tables and put them in database object, return <see cref="IEnumerable{T}"/> contain all Tables
        /// </summary>
        /// <param name="dataBase">the DataBase object</param>
        public static IEnumerable<SQLiteTable> DataBase(SQLiteDataBase dataBase)
        {
            var command = new SQLiteCommand(dataBase.Connection) {CommandText = SQLiteQuery.GetTables};
            var reader = command.ExecuteReader();

            while (reader.Read())
                yield return new SQLiteTable(dataBase, reader.GetString(0));
            reader.Close();
        }

        /// <summary>
        /// DisCover all the DataBase Columns and put them in Table object, return <see cref="IEnumerable{T}"/> contain all Columns
        /// </summary>
        /// <param name="table">the Table object</param>
        public static IEnumerable<SQLiteTableColumn> TableColumns(SQLiteTable table) =>
            table.GetObjectArray2D(table.ExecuteReaderQuery(table.Query.GetTableColumns))
                .Select(column => new SQLiteTableColumn(column[1].ToString(), column[2].ToString()));
    }
}
