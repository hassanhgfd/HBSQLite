using System;

namespace HBSQLite
{
    /// <summary>
    /// Provide Properties and methods For work with table Columns
    /// </summary>
    public class SQLiteTableColumn : ISQLiteTableColumn
    {
        /// <summary>
        /// converts this 
        /// </summary>
        /// <param name="tableColumn"></param>
        public static implicit operator string(SQLiteTableColumn tableColumn) => tableColumn.ToString();

        /// <summary>
        /// the name of this column
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// the name of the type of this column
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// initialize new instance of <see cref="SQLiteTableColumn"/> by Table object and the name 
        /// </summary>
        /// <param name="name">the name of this column</param>
        /// <param name="typeName">the name of the type of this column</param>
        public SQLiteTableColumn(string name, string typeName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("message", nameof(name));

            Name = name;
            TypeName = typeName;
        }

        /// <summary>
        /// convert this object to string by returning <see cref="Name"/>
        /// </summary>
        /// <returns>return <see cref="Name"/></returns>
        public override string ToString() => $" {Name} ";
    }
}
