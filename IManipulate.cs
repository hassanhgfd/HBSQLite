using System;
using System.ComponentModel;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// provide methods and properties for manipulating the table
    /// </summary>
    public interface IManipulate
    {
        /// <summary>
        /// instance of <see cref="SQLiteQuery"/> this Make queries For this object
        /// </summary>
        SQLiteQuery Query { get; }

        /// <summary>
        /// the DataBase object for this class
        /// </summary>
        SQLiteDataBase SQLiteDataBase { get; }

        /// <summary>
        /// Insert Into DataBase Depending on the object
        /// </summary>
        /// <typeparam name="TTable">class or struct</typeparam>
        /// <param name="values">object contain properties the name of them are the same name of table columns
        /// and the value its the value that have to be inserted</param>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        void Insert<TTable>(TTable values);

        /// <summary>
        /// this Method Update Data in the Table
        /// </summary>
        /// <param name="tableColumns">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="condition">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        void UpDate<TTable>(TTable tableColumns, TTable condition);

        /// <summary>
        /// this Method Update Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
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
    }
}
