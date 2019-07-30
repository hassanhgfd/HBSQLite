using System;
using System.ComponentModel;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// provide methods and properties for manipulating queries for the table
    /// </summary>
    public interface IQueryManipulate
    {
        /// <summary>
        /// the Table Name which the Queries created for it
        /// </summary>
        SQLiteTable Table { get; }

        /// <summary>
        /// Make query Insert Into DataBase Depending on the object
        /// </summary>
        /// <typeparam name="TTable">class or struct</typeparam>
        /// <param name="values">object contain properties the name of them are the same name of table columns
        /// and the value its the value that have to be inserted</param>
        /// <exception cref="SQLiteTableColumnNotFoundException"></exception>
        string Insert<TTable>(TTable values);

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
        string UpDate<TTable>(TTable tableColumns, TTable condition,
            ConditionRelationShip conditionRelationShip);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="condition">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        string UpDate<TTable>(TTable tableColumns, TTable condition);

        /// <summary>
        /// create Update query for Update all Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="tableColumns">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        string UpDate<TTable>(TTable tableColumns);

        /// <summary>
        /// create delete query for delete all Data in the Table
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="condition">Class Or Struct contains properties their
        ///     Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        /// <param name="conditionRelationShip"></param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        string Delete<TTable>(TTable condition, ConditionRelationShip conditionRelationShip);

        /// <summary>
        /// create Delete query for Delete Data in the Table by the condition and
        /// if there is more than one field used the <see cref="ConditionRelationShip"/> = AND
        /// </summary>
        /// <typeparam name="TTable">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; so the value of them will be updated in the DataBase</typeparam>
        /// <param name="condition">Class Or Struct contains properties their
        /// Names are the same withe the Column Names; the properties type must be <see cref="ColumnValue{T}"/></param>
        string Delete<TTable>(TTable condition);

        /// <summary>
        /// create delete query for delete all Data in the Table
        /// </summary>
        string Delete();

    }
}
