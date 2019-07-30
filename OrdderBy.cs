using System;
using System.Collections.Generic;

namespace HBSQLite
{
    /// <summary>
    /// provide methods and properties to create order by object
    /// </summary>
    public class OrderBy : IOrderBy
    {
        /// <summary>
        /// convert this class to string
        /// </summary>
        /// <param name="orderBy">the object to convert</param>
        public static implicit operator string(OrderBy orderBy) => orderBy.ToString();

        /// <summary>
        /// the columns that will be used for sorting
        /// </summary>
        public List<SQLiteTableColumn> Columns { get; } = new List<SQLiteTableColumn>();
        
        /// <summary>
        /// the way to oder 
        /// </summary>
        public OrderByWay OrderByWay { get; set; }

        /// <summary>
        /// initialize new instance of <see cref="SQLiteTableColumn"/> with full data
        /// </summary>
        /// <param name="columns">the columns that will be used for sorting</param>
        /// <param name="orderByWay">the way to oder</param>
        public OrderBy(IEnumerable<SQLiteTableColumn> columns, OrderByWay orderByWay)
        {
            Columns = new List<SQLiteTableColumn>(columns);
            OrderByWay = orderByWay;
        }

        /// <summary>
        /// initialize new instance of <see cref="SQLiteTableColumn"/> with one column
        /// </summary>
        /// <param name="column">the column that will be used for sorting</param>
        /// <param name="orderByWay">the way to oder</param>
        public OrderBy(SQLiteTableColumn column, OrderByWay orderByWay)
        {
            if (column == null) throw new ArgumentNullException(nameof(column));
            Add(column);
            OrderByWay = orderByWay;
        }


        /// <summary>
        /// initialize new instance of <see cref="SQLiteTableColumn"/> with order state
        /// </summary>
        /// <param name="orderByWay">the way to oder</param>
        public OrderBy(OrderByWay orderByWay) => OrderByWay = orderByWay;

        /// <summary>
        /// add new column to the list
        /// </summary>
        /// <param name="columns">the column that will be used for sorting</param>
        public void Add(SQLiteTableColumn columns)
        {
            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            Columns.Add(columns);
        }

        /// <summary>
        /// convert this object to string that can used in query
        /// </summary>
        /// <returns>string can used in query</returns>
        public override string ToString()
        {
            switch (OrderByWay)
            {
                case OrderByWay.ASC:
                    return $" order by {string.Join(",", Columns)} {OrderByWay.ASC} ";
                case OrderByWay.DESC:
                    return $" order by {string.Join(",", Columns)} {OrderByWay.DESC} ";
                default:
                    throw new ArgumentNullException(nameof(OrderByWay));
            }
        }
    }
}
