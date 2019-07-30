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
    /// the interface for <see cref="SQLiteTable"/> provide properties and method for work with the Table
    /// </summary>
    public interface ISQLiteTable
    {
        /// <summary>
        /// the DataBase object for this class
        /// </summary>
        SQLiteDataBase SQLiteDataBase { get; }

        /// <summary>
        /// instance of <see cref="SQLiteQuery"/> this Make queries For this object
        /// </summary>
        SQLiteQuery Query { get; }

        /// <summary>
        /// the Name Of this Table
        /// </summary>
        string Name { get; }

        /// <summary>
        /// the Table Columns as <see cref="SQLiteTableColumn"/>
        /// </summary>
        List<SQLiteTableColumn> Columns { get; }

        /// <summary>
        /// Get the Column by it name
        /// </summary>
        /// <param name="columnName">the name of Column</param>
        /// <returns>the Column object</returns>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        SQLiteTableColumn this[string columnName] { get; }

        /// <summary>
        /// execute queries that not read any data
        /// </summary>
        /// <param name="query">the query for execute</param>
        void ExecuteNonQuery(string query);

        /// <summary>
        /// Execute read query and return the result as <see cref="SQLiteDataReader"/>
        /// </summary>
        /// <param name="query">the Query for execute</param>
        /// <returns></returns>
        IDataReader ExecuteReaderQuery(string query);

        /// <summary>
        /// Insert Into DataBase Depending on the object
        /// </summary>
        /// <typeparam name="TTable">class or struct</typeparam>
        /// <param name="values">object contain properties the name of them are the same name of table columns
        /// and the value its the value that have to be inserted</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        void Insert<TTable>(TTable values);

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
        void UpDate<TTable>(TTable tableColumns, TTable condition, ConditionRelationShip conditionRelationShip);

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
        void UpDate<TTable>(TTable tableColumns, TTable condition);

        /// <summary>
        /// this Method Update Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <inheritdoc cref="IManipulate"/>
        void UpDate<TTable>(TTable tableColumns);

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
        void Delete<TTable>(TTable condition, ConditionRelationShip conditionRelationShip);

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
        void Delete<TTable>(TTable condition);

        /// <summary>
        /// this Method Delete Data in the Table
        /// </summary>
        /// <inheritdoc cref="IManipulate"/>
        void Delete();

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
        DataTable ReadTable<TCondition>(TCondition condition);

        /// <inheritdoc cref="IReadData"/>
        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip"></param>
        /// <returns></returns>
        DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip);

        /// <inheritdoc cref="IReadAllData"/>
        /// <summary>
        /// Read All the Data From the DataBase And Return them <see cref="DataTable"/>
        /// </summary>
        /// <returns></returns>
        DataTable ReadTable();

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
        object[][] Read<TCondition>(TCondition condition);

        /// <inheritdoc cref="IReadData"/>
        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip"></param>
        /// <returns></returns>
        object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip);

        /// <inheritdoc cref="IReadAllData"/>
        /// <summary>
        /// Read All the Data From the DataBase And Return them array of objects
        /// </summary>
        /// <returns></returns>
        object[][] Read();

        /// <summary>
        /// convert the object to string, return the name of the object
        /// </summary>
        /// <returns>return the name of the object</returns>
        string ToString();
    }
}