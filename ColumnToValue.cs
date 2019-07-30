namespace HBSQLite
{
    /// <summary>
    /// this enumerator contain the states to connect between the 
    /// <see cref="SQLiteTableColumn" /> and it value in condition within the Query
    /// </summary>
    public enum ColumnToValue
    {
        /// <summary>
        /// this put =
        /// </summary>
        Equal,
        /// <summary>
        /// this put like
        /// </summary>
        Like,
        /// <summary>
        /// this put not like
        /// </summary>
        NotLike,
        /// <summary>
        /// this put &gt;
        /// </summary>
        GreaterThan,
        /// <summary>
        /// this put  &gt;=
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// this put Smaller Than Mark
        /// </summary>
        SmallerThan,

        /// <summary>
        /// this put Smaller Than or Equal Mark
        /// </summary>
        SmallerThanOrEqual
    }
}
