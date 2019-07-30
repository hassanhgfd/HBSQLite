using System.Collections.Generic;
using System.Data;

namespace HBSQLite
{
    /// <summary>
    /// provide methods for read data from the database
    /// </summary>
    public interface IReadData
    {
        /// <summary>
        /// Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip"/>
        /// going to be (And), Return the result as <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <returns></returns>
        DataTable ReadTable<TCondition>(TCondition condition);

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result <see cref="DataTable"/>
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <returns></returns>
        DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip);

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
        DataTable ReadTable<TCondition>(TCondition condition, SQLiteTableColumn column);

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
        DataTable ReadTable<TCondition>(TCondition condition, SQLiteTableColumn column, OrderByWay orderByWay);

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
        DataTable ReadTable<TCondition>(TCondition condition, OrderBy orderBy);

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
        DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column);

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
        DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column, OrderByWay orderByWay);

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
        DataTable ReadTable<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns);

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
        DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip, IEnumerable<SQLiteTableColumn> columns);

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
        DataTable ReadTable<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy);

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
        DataTable ReadTable<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy);

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

        /// <summary>
        /// Read the Data From the DataBase by condition, Return the result as array of objects
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip">the relationship between the parts of the condition</param>
        /// <returns></returns>
        object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip);

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
        object[] Read<TCondition>(TCondition condition, SQLiteTableColumn column);

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
        object[] Read<TCondition>(TCondition condition, SQLiteTableColumn column, OrderByWay orderByWay);

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
        object[][] Read<TCondition>(TCondition condition, OrderBy orderBy);

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
        object[] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column);

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
        object[] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            SQLiteTableColumn column, OrderByWay orderByWay);

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
        object[][] Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns);

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
        object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip, IEnumerable<SQLiteTableColumn> columns);

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
        object[][] Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy);

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
        object[][] Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy);

    }
}
