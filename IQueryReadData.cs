using System.Collections.Generic;
using System.Data;

namespace HBSQLite
{
    /// <summary>
    /// provide methods for read data from the database
    /// </summary>
    public interface IQueryReadData
    {
        /// <summary>
        /// make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And)
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <returns></returns>
        string Read<TCondition>(TCondition condition);

        /// <summary>
        /// make query for Read the Data From the DataBase by condition
        /// </summary>
        /// <typeparam name="TCondition"> class or struct contain
        /// properties of type <see cref="ColumnValue{T}" />
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="conditionRelationShip"></param>
        /// <returns></returns>
        string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip);

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
        string Read<TCondition>(TCondition condition, ref SQLiteTableColumn column);

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
        string Read<TCondition>(TCondition condition, ref SQLiteTableColumn column, OrderByWay orderByWay);

        /// <summary>
        ///  make query for Read the Data From the DataBase by condition has one field and
        /// if it's has more the <see cref="ConditionRelationShip" />
        /// going to be (And) and order the result depending on the <see cref="OrderBy"/> object
        /// </summary>
        /// <typeparam name="TCondition">class or struct contain
        /// properties of type <see cref="ColumnValue{T}"/>
        /// the name of the properties as the names of the table columns</typeparam>
        /// <param name="condition">the object that will be used for making conditions</param>
        /// <param name="orderBy">instance of <see cref="OrderBy"/> for order the result </param>
        /// <returns></returns>
        string Read<TCondition>(TCondition condition, OrderBy orderBy);

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
        string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            ref SQLiteTableColumn column);

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
        string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            ref SQLiteTableColumn column, OrderByWay orderByWay);

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
        string Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns);

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
        string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns);

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
        string Read<TCondition>(TCondition condition, IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy);

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
        string Read<TCondition>(TCondition condition, ConditionRelationShip conditionRelationShip,
            IEnumerable<SQLiteTableColumn> columns, OrderBy orderBy);
    }
}