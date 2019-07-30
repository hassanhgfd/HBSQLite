using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// Provide All the Methods And Properties for doing all the process on th SQLite Table
    /// </summary>
    /// <inheritdoc cref="IManipulate"/>
    public class SQLiteTable : IManipulate, IManipulateAllDelete, IReadData, IReadAllData, ISQLiteTable
    {
        /// <summary>
        /// convert the <see cref="SQLiteTable"/> to string
        /// </summary>
        /// <param name="table">the table to convert</param>
        public static implicit operator string(SQLiteTable table) => table.ToString();

        #region Properties

        /// <inheritdoc cref="ISQLiteTable" />
        /// <summary>
        /// instance of <see cref="SQLiteQuery"/> this Make queries For this object
        /// </summary>
        public SQLiteQuery Query { get; }

        /// <summary>
        /// the DataBase object for this class
        /// </summary>
        public SQLiteDataBase SQLiteDataBase { get; }

        /// <inheritdoc cref="ISQLiteTable" />
        /// <summary>
        /// the Name Of this Table
        /// </summary>
        public string Name { get; }

        /// <inheritdoc cref="ISQLiteTable" />
        /// <summary>
        /// the Table Columns as <see cref="SQLiteTableColumn"/>
        /// </summary>
        public List<SQLiteTableColumn> Columns { get; } = new List<SQLiteTableColumn>();

        #endregion

        #region Constructors

        /// <summary>
        /// create New instance of <see cref="SQLiteTable"/>
        /// </summary>
        /// <param name="sqLiteDataBase">the DataBase object for this class</param>
        /// <param name="name">the Name Of this Table</param>
        public SQLiteTable(SQLiteDataBase sqLiteDataBase, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("message", nameof(name));

            SQLiteDataBase = sqLiteDataBase ?? throw new ArgumentNullException(nameof(sqLiteDataBase));
            Name = name;

            Query = new SQLiteQuery(this);

            Columns.AddRange(Discover.TableColumns(this));
        }

        #endregion

        #region Indexers

        /// <inheritdoc />
        /// <summary>
        /// Get the Column by it name
        /// </summary>
        /// <param name="columnName">the name of Column</param>
        /// <returns>the Column object</returns>
        /// <exception cref="T:HBSQLite.Exception.SQLiteTableColumnNotFoundException"></exception>
        public SQLiteTableColumn this[string columnName]
        {
            get
            {
                return Columns.Find((column) => column.Name.Equals(columnName))
                       ?? throw new SQLiteTableColumnNotFoundException(columnName);
            }
        }

        #endregion

        #region Methods

        #region ExecuteMethods

        private SQLiteCommand CreateCommand() => new SQLiteCommand(SQLiteDataBase.Connection);

        /// <inheritdoc cref="ISQLiteTable" />
        /// <summary>
        /// execute queries that not read any data
        /// </summary>
        /// <param name="query">the query for execute</param>
        public async void ExecuteNonQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(query));

            var command = CreateCommand();
            command.CommandText = query;
            await command.ExecuteNonQueryAsync();
        }

        /// <inheritdoc cref="ISQLiteTable" />
        /// <summary>
        /// Execute read query and return the result as <see cref="DbDataReader"/>
        /// </summary>
        /// <param name="query">the Query for execute</param>
        /// <returns></returns>
        public IDataReader ExecuteReaderQuery(string query)
        {
            var command = CreateCommand();
            command.CommandText = query;
            var dataReader = command.ExecuteReaderAsync().Result;
            return dataReader;
        }

        #endregion

        #region Private

        private void CloseReader(IDataReader dataReader)
        {
            if (!dataReader.IsClosed)
                dataReader.Close();
        }

        #endregion

        #region Manuplate Data Methods

        /// <summary>
        /// Insert Into DataBase Depending on the object
        /// </summary>
        /// <typeparam name="TTable">class or struct</typeparam>
        /// <param name="values">object contain properties the name of them are the same name of table columns
        /// and the value its the value that have to be inserted</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        public void Insert<TTable>(TTable values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            ExecuteNonQuery(Query.Insert(values));
        }

        /// <summary>
        /// this Method Update Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="condition">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="conditionRelationShip">the relation ship between conditions in the Query</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        public void UpDate<TTable>(TTable tableColumns, TTable condition, ConditionRelationShip conditionRelationShip)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));

            ExecuteNonQuery(Query.UpDate(tableColumns, condition, conditionRelationShip));
        }

        /// <summary>
        /// this Method Update Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="condition">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        public void UpDate<TTable>(TTable tableColumns, TTable condition)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            if (condition == null) throw new ArgumentNullException(nameof(condition));

            ExecuteNonQuery(Query.UpDate(tableColumns, condition));
        }

        /// <summary>
        /// this Method Update Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        public void UpDate<TTable>(TTable tableColumns)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            ExecuteNonQuery(Query.UpDate(tableColumns));
        }

        /// <summary>
        /// this Method Delete Data in the Table by one column and
        /// if they are more the <see cref="ConditionRelationShip"/> become AND
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="condition">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="conditionRelationShip">the relation ship between conditions in the Query</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        public void Delete<TTable>(TTable condition, ConditionRelationShip conditionRelationShip)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            ExecuteNonQuery(Query.Delete(condition, conditionRelationShip));
        }

        /// <summary>
        /// this Method Delete Data in the Table by one column and
        /// if they are more the <see cref="ConditionRelationShip"/> become AND
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="condition">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        public void Delete<TTable>(TTable condition)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            ExecuteNonQuery(Query.Delete(condition));
        }

        /// <summary>
        /// this Method Delete Data in the Table
        /// </summary>
        /// <inheritdoc cref="IManipulate"/>
        public void Delete() => ExecuteNonQuery(Query.Delete());

        #endregion

        #region Fetch Data Reader

        /// <summary>
        /// take the <see cref="DbDataReader"/> and convert it to <see cref="DataTable"/>
        /// </summary>
        /// <param name="dataReader">the data reader object getting information</param>
        /// <returns><see cref="DataTable"/> build from dataReader</returns>
        public DataTable GetDataTable(IDataReader dataReader)
        {
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            CloseReader(dataReader);
            return dataTable;
        }
        
        /// <summary>
        /// take the <see cref="IDataReader"/> and convert it to <see cref="object"/> 2 Dimensional Array
        /// </summary>
        /// <param name="dataReader">the data reader object getting information</param>
        /// <returns><see cref="object"/> 2 Dimensional Array build from dataReader</returns>
        public object[][] GetObjectArray2D(IDataReader dataReader)
        {
            var result = new List<object[]>();
            while (dataReader.Read())
            {
                var row = new object[dataReader.FieldCount];
                for (var columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    row[columnIndex] = dataReader.GetValue(columnIndex);
                result.Add(row);
            }
            CloseReader(dataReader);
            return result.ToArray();
        }
        
        /// <summary>
        /// take the <see cref="DbDataReader"/> and convert it to <see cref="object"/> Array
        /// </summary>
        /// <param name="dataReader">the data reader object getting information</param>
        /// <returns><see cref="object"/> Array build from dataReader</returns>
        public object[] GetObjectArray(IDataReader dataReader)
        {
            var result = new List<object>();

            while (dataReader.Read())
                result.Add(dataReader.GetValue(0));
            CloseReader(dataReader);
            return result.ToArray();
        }

        #endregion

        #region IReadData Methods

        /// <inheritdoc cref="IReadData"/>
        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="T:HBSQLite.ConditionRelationShip" />
        /// going to be (And), Return the result as <see cref="T:System.Data.DataTable" />
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="T:HBSQLite.ColumnValue`1" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (condition is IEnumerable<SQLiteTableColumn> columns)
                return ReadTable(columns);
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition)));
        }

        /// <inheritdoc cref="IReadData"/>
        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="column"> the column that will be returned </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, SQLiteTableColumn column)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (column == null) throw new ArgumentNullException(nameof(column));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, ref column)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="column"> the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, SQLiteTableColumn column,
            OrderByWay orderByWay)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (column == null) throw new ArgumentNullException(nameof(column));
            if (!Enum.IsDefined(typeof(OrderByWay), orderByWay))
                throw new InvalidEnumArgumentException(nameof(orderByWay), (int) orderByWay, typeof(OrderByWay));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, ref column, orderByWay)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, OrderBy orderBy)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            if (condition is IEnumerable<SQLiteTableColumn> columns)
                return ReadTable(columns, orderBy);
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, orderBy)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="column"> the column that will be returned </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, ref column)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="column"> the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column, OrderByWay orderByWay)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            if (!Enum.IsDefined(typeof(OrderByWay), orderByWay))
                throw new InvalidEnumArgumentException(nameof(orderByWay), (int) orderByWay, typeof(OrderByWay));
            return GetDataTable(
                ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, ref column, orderByWay)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="columns"> the columns that will be returned from the table </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, columns)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="columns"> the columns that will be returned from the table </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, columns)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="columns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns,
            OrderBy orderBy)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, columns, orderBy)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="columns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns,
            OrderBy orderBy)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetDataTable(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, columns, orderBy)));
        }

        /// <inheritdoc cref="IReadAllData"/>
        /// <summary>
        /// Read All the Data From the DataBase And Return them <see cref="DataTable"/>
        /// </summary>
        /// <returns></returns>
        public DataTable ReadTable() => GetDataTable(ExecuteReaderQuery(Query.Read()));

        /// <summary>
        /// Read one column From the Table And Return them <see cref="DataTable"/>
        /// </summary>
        /// <param name="tableColumn"> the column that will be returned </param>
        /// <returns></returns>
        public DataTable ReadTable(SQLiteTableColumn tableColumn)
        {
            if (tableColumn == null) throw new ArgumentNullException(nameof(tableColumn));
            return GetDataTable(ExecuteReaderQuery(Query.Read(tableColumn)));
        }

        /// <summary>
        /// Read one column From the Table And Return them <see cref="DataTable"/>
        /// </summary>
        /// <param name="tableColumn">the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        public DataTable ReadTable(SQLiteTableColumn tableColumn, OrderByWay orderByWay)
        {
            if (tableColumn == null) throw new ArgumentNullException(nameof(tableColumn));
            if (!Enum.IsDefined(typeof(OrderByWay), orderByWay))
                throw new InvalidEnumArgumentException(nameof(orderByWay), (int) orderByWay, typeof(OrderByWay));
            return GetDataTable(ExecuteReaderQuery(Query.Read(ref tableColumn, orderByWay)));
        }

        /// <summary>
        /// Read more than one column From the Table And Return them <see cref="DataTable"/>
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns">the columns that will be returned </param>
        /// <returns></returns>
        public DataTable ReadTable(IEnumerable<SQLiteTableColumn> tableColumns)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            return GetDataTable(ExecuteReaderQuery(Query.Read(tableColumns)));
        }

        /// <summary>
        /// Read more than one column From the Table And Return them <see cref="DataTable"/>
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public DataTable ReadTable(IEnumerable<SQLiteTableColumn> tableColumns, OrderBy orderBy)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            return GetDataTable(ExecuteReaderQuery(Query.Read(tableColumns, orderBy)));
        }

        /// <inheritdoc cref="IReadData"/>
        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (condition is IEnumerable<SQLiteTableColumn> columns)
                return Read(columns);
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition)));
        }

        /// <inheritdoc cref="IReadData"/>
        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="column"> the column that will be returned </param>
        /// <returns></returns>
        public object[] Read<TCondition>(TCondition condition, SQLiteTableColumn column)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (column == null) throw new ArgumentNullException(nameof(column));
            return GetObjectArray(ExecuteReaderQuery(Query.Read(condition, ref column)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="column"> the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        public object[] Read<TCondition>(TCondition condition, SQLiteTableColumn column, OrderByWay orderByWay)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (column == null) throw new ArgumentNullException(nameof(column));
            if (!Enum.IsDefined(typeof(OrderByWay), orderByWay))
                throw new InvalidEnumArgumentException(nameof(orderByWay), (int) orderByWay, typeof(OrderByWay));
            return GetObjectArray(ExecuteReaderQuery(Query.Read(condition, ref column, orderByWay)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition, OrderBy orderBy)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            if (condition is IEnumerable<SQLiteTableColumn> columns)
                return Read(columns, orderBy);
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition, orderBy)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="column"> the column that will be returned </param>
        /// <returns></returns>
        public object[] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetObjectArray(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, ref column)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="column"> the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        public object[] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column, OrderByWay orderByWay)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            if (!Enum.IsDefined(typeof(OrderByWay), orderByWay))
                throw new InvalidEnumArgumentException(nameof(orderByWay), (int) orderByWay, typeof(OrderByWay));
            return GetObjectArray(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, ref column,
                orderByWay)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="columns"> the columns that will be returned from the table </param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition, columns)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="columns"> the columns that will be returned from the table </param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, columns)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="columns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns,
            OrderBy orderBy)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition, columns, orderBy)));
        }

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <param name="columns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy "/> for order the result </param>
        /// <returns></returns>
        public object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns,
            OrderBy orderBy)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (columns == null) throw new ArgumentNullException(nameof(columns));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            if (!Enum.IsDefined(typeof(ConditionRelationShip), conditionRelationShip))
                throw new InvalidEnumArgumentException(nameof(conditionRelationShip), (int) conditionRelationShip,
                    typeof(ConditionRelationShip));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(condition, conditionRelationShip, columns, orderBy)));
        }

        /// <inheritdoc cref="IReadAllData"/>
        /// <summary>
        /// Read All the Data From the DataBase And Return them array of objects
        /// </summary>
        /// <returns></returns>
        public object[][] Read() => GetObjectArray2D(ExecuteReaderQuery(Query.Read()));

        /// <summary>
        /// Read one column From the Table And Return them array of objects
        /// </summary>
        /// <param name="tableColumn"> the column that will be returned </param>
        /// <returns></returns>
        public object[] Read(SQLiteTableColumn tableColumn)
        {
            if (tableColumn == null) throw new ArgumentNullException(nameof(tableColumn));
            return GetObjectArray(ExecuteReaderQuery(Query.Read(tableColumn)));
        }

        /// <summary>
        /// Read one column From the Table And Return them array of objects
        /// </summary>
        /// <param name="tableColumn"> the column that will be returned </param>
        /// <param name="orderByWay">the way to order the result ASC Or DESC</param>
        /// <returns></returns>
        public object[] Read(SQLiteTableColumn tableColumn, OrderByWay orderByWay)
        {
            if (tableColumn == null) throw new ArgumentNullException(nameof(tableColumn));
            if (!Enum.IsDefined(typeof(OrderByWay), orderByWay))
                throw new InvalidEnumArgumentException(nameof(orderByWay), (int) orderByWay, typeof(OrderByWay));
            return GetObjectArray(ExecuteReaderQuery(Query.Read(ref tableColumn, orderByWay)));
        }

        /// <summary>
        /// Read more than one column From the Table And Return them array of objects
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// <param name="tableColumns"> the columns that will be returned </param>
        /// </summary>
        /// <returns></returns>
        public object[][] Read(IEnumerable<SQLiteTableColumn> tableColumns)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(tableColumns)));
        }

        /// <summary>
        /// Read more than one column From the Table And Return them array of objects
        /// contain tableColumns that inserted in <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="tableColumns"> the columns that will be returned </param>
        /// <param name="orderBy">instance of <see cref="OrderBy"/> for order the result </param>
        /// <returns>array of objects</returns>
        public object[][] Read(IEnumerable<SQLiteTableColumn> tableColumns, OrderBy orderBy)
        {
            if (tableColumns == null) throw new ArgumentNullException(nameof(tableColumns));
            if (orderBy == null) throw new ArgumentNullException(nameof(orderBy));
            return GetObjectArray2D(ExecuteReaderQuery(Query.Read(tableColumns, orderBy)));
        }

        #endregion

        #region Override

        /// <inheritdoc cref="ISQLiteTable" />
        /// <summary>
        /// convert the object to string, return the name of the object
        /// </summary>
        /// <returns>return the name of the object</returns>
        public override string ToString() => Name;


        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var table = obj as SQLiteTable;
            if(table is null)return  false;
            return SQLiteDataBase == table.SQLiteDataBase && Name == table.Name && Columns == table.Columns;
        }

        #endregion

        #endregion

    }
}
