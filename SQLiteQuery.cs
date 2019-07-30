using System;
using System.Collections.Generic;

namespace HBSQLite
{
    /// <summary>
    /// Provide methods and properties for make and execute queries on SQLiteDataBase 
    /// </summary>
    public class SQLiteQuery : IQueryManipulate, IQueryReadAllData, IQueryReadData
    {
        #region Properties

        /// <summary>
        /// the Query to get SQLite DataBase Tables
        /// </summary>
        public static string GetTables { get; } = "SELECT name FROM sqlite_master WHERE type like '%table%';";

        /// <summary>
        /// the Query to get SQLite Table Columns
        /// </summary>
        public string GetTableColumns => $"PRAGMA table_info({Table});";

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// the Table Name which the Queries created for it
        /// </summary>
        public SQLiteTable Table { get; }

        /// <summary>
        /// instance of <see cref="ObjectReader"/> for reading objects
        /// </summary>
        public ObjectReader ObjectReader { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// create new instance of <see cref="SQLiteDataBase" /> initialized to Work with specific Table
        /// </summary>
        /// <param name="table">the Table Name that will Make Queries for it</param>
        public SQLiteQuery(SQLiteTable table)
        {
            Table = table ?? throw new ArgumentException("message", nameof(table));
            ObjectReader = new ObjectReader(Table);
        }

        #endregion

        #region Methods

        private string QueryCondition<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip) =>
            $" where {string.Join($" {conditionRelationShip.ToString()} ", ObjectReader.GetPropertiesColumnValues(condition, false))}";

        #region Mainpulate Methods

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create query to insert in the <see cref="Table"/>, return the query to execute 
        /// </summary>
        /// <typeparam name="TTable">Class or struct contain the name of column as property 
        /// and the value of the column as value of pro property</typeparam>
        /// <param name="values">the object and the values</param>
        /// <returns>return the query to execute</returns>
        public string Insert<TTable>(TTable values)
        {
            var (columnName, columnValue) = ObjectReader.GetPropertiesNamesValues(values);

            return $"insert into {Table} ({string.Join(",", columnName)}) values ({string.Join(",", columnValue)})";
        }

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create Update query for Update Data in the Table using multi condition and the relationships between them  
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="condition">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="conditionRelationShip">the relation ship between conditions in the Query</param>
        public string UpDate<TTable>(TTable tableColumns, TTable condition,
            ConditionRelationShip conditionRelationShip) =>
            $"{UpDate(tableColumns)}{QueryCondition(condition, conditionRelationShip)}";

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create Update query for Update Data in the Table using multi condition and the relationships between them  
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="condition">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        public string UpDate<TTable>(TTable tableColumns, TTable condition) =>
            $"{UpDate(tableColumns)}{QueryCondition(condition, ConditionRelationShip.And)}";

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create Update query for Update all Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        public string UpDate<TTable>(TTable tableColumns) =>
            $"update {Table} set {string.Join(",", ObjectReader.GetPropertiesColumnValues(tableColumns, true))}";

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create delete query for delete Data in the Table using multi condition and the relationships between them  
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="condition">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="T:HBSQLite.ColumnValue`1" /></param>
        /// <param name="conditionRelationShip">the relation ship between conditions in the Query</param>
        public string Delete<TTable>(TTable condition, ConditionRelationShip conditionRelationShip) =>
            $"{Delete()}{QueryCondition(condition, conditionRelationShip)}";

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create delete query for delete Data in the Table using multi condition and the relationships between them  
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="condition">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="T:HBSQLite.ColumnValue`1" /></param>
        public string Delete<TTable>(TTable condition) =>
            $"{Delete()}{QueryCondition(condition, ConditionRelationShip.And)}";

        /// <inheritdoc cref="IQueryManipulate"/>
        /// <summary>
        /// create delete query for delete all Data in the Table
        /// </summary>
        public string Delete() => $"delete from {Table}";

        #endregion

        #region Read

        /// <inheritdoc cref="IQueryReadAllData" />
        /// <summary>
        /// make query for Read All the Data From the Table
        /// </summary>
        /// <returns></returns>
        public string Read() => $"select * from {Table} ";

        /// <summary>
        /// make query for Read All the Data From the Table
        /// </summary>
        /// <param name="tableColumn">the column that will be returned </param>
        /// <returns>query for Read All the Data From the Table</returns>
        public string Read(ref SQLiteTableColumn tableColumn) => $"select {tableColumn} from {Table} ";

        /// <summary>
        /// make query for Read one column From the Table
        /// </summary>
        /// <param name="tableColumn">the column that will be returned </param>
        /// <param name="orderByWay"></param>
        /// <returns>query for Read All the Data From the Table</returns>
        public string Read(ref SQLiteTableColumn tableColumn, OrderByWay orderByWay) =>
            $"{Read(ref tableColumn)}{new OrderBy(tableColumn, orderByWay)}";

        /// <summary>
        /// make query for Read more than one column From the Table
        /// </summary>
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// <param name="tableColumns">the columns that will be returned </param>
        /// <returns>query for Read All the Data From the Table</returns>
        public string Read(IEnumerable<SQLiteTableColumn> tableColumns) =>
            $"select {string.Join(",", tableColumns)} from {Table} ";

        /// <summary>
        /// make query for Read more than one column From the Table
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns">the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy"/> for order the result </param>
        /// <returns>query for Read All the Data From the Table</returns>
        public string Read(IEnumerable<SQLiteTableColumn> tableColumns, OrderBy orderBy) =>
            $"{Read(tableColumns)}{orderBy}";

        /// <inheritdoc cref="IQueryReadData"/>
        /// <summary>
        ///  make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And)
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="T:HBSQLite.ColumnValue`1" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition)
        {
            if (condition is IEnumerable<SQLiteTableColumn> columns)
                return Read(columns);
            if (condition is SQLiteTableColumn)
            {
                var column = (SQLiteTableColumn)Convert.ChangeType(condition, typeof(SQLiteTableColumn));
                return Read(ref column);
            }
            return $"{Read()}{QueryCondition(condition, ConditionRelationShip.And)}";
        }

        /// <inheritdoc cref="IQueryReadData"/>
        /// <summary>
        /// make query for Read the Data From the DataBase by condition
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip"></param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip) =>
            $"{Read()}{QueryCondition(condition, conditionRelationShip)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And) and the result will be one column
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="column"> the column that will be returned </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ref SQLiteTableColumn column) =>
            $"{Read(ref column)}{QueryCondition(condition, ConditionRelationShip.And)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And) and the result will be one column
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="column"> the column that will be returned </param>
        /// <param name="orderByWay">instance of <see cref="OrderByWay"/> for order the result </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ref SQLiteTableColumn column, OrderByWay orderByWay) =>
            $"{Read(ref column)}{QueryCondition(condition, ConditionRelationShip.And)}{new OrderBy(column, orderByWay)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And) and order the result depending on the <see cref="OrderBy"/> object
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, OrderBy orderBy)
        {
            if (condition is IEnumerable<SQLiteTableColumn> columns)
                return Read(columns, orderBy);
            return $"{Read()}{QueryCondition(condition, ConditionRelationShip.And)}{orderBy}";
        }

        /// <summary>
        /// make query for Read the Data From the DataBase by condition and the result will be one column
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="column"> the column that will be returned </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            ref SQLiteTableColumn column) => $"{Read(ref column)}{QueryCondition(condition, conditionRelationShip)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition and the result will be one column
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="column"> the column that will be returned </param>
        /// <param name="orderByWay">instance of <see cref="OrderByWay"/> for order the result </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            ref SQLiteTableColumn column,
            OrderByWay orderByWay) =>
            $"{Read(ref column)}{QueryCondition(condition, conditionRelationShip)}{new OrderBy(column, orderByWay)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And) and the result will be the columns that putted in the columns parameter
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="columns"> the columns that will be returned from the table </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns) =>
            $"{Read(columns)}{QueryCondition(condition, ConditionRelationShip.And)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition
        /// and the result will be table by the same columns that inputted
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="columns"> the columns that will be returned from the table </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns) =>
            $"{Read(columns)}{QueryCondition(condition, conditionRelationShip)}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And) and the result will be the columns that putted in the columns parameter
        /// and order the result depending on the <see cref="OrderBy"/> object
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="columns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy) =>
            $"{Read(columns)}{QueryCondition(condition, ConditionRelationShip.And)}{orderBy}";

        /// <summary>
        /// make query for Read the Data From the DataBase by condition
        /// and the result will be table by the same columns that inputted
        /// and order the result depending on the <see cref="OrderBy"/> object
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="columns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy) =>
            $"{Read(columns)}{QueryCondition(condition, conditionRelationShip)}{orderBy}";

        #endregion

        #endregion

    }
}
