namespace HBSQLite
{
    /// <summary>
    /// provide methods for browsing data base
    /// </summary>
    public interface IBrowse
    {
        /// <summary>
        /// the DataSource For this ReaderIndex
        /// </summary>
        string DataSource { get; }

        /// <summary>
        /// determine weather this in browsing mode or not
        /// </summary>
        bool IsBrowsing { get; }

        /// <summary>
        /// determine weather this in Bake up database or not
        /// </summary>
        bool IsBakeUp { get; }

        /// <summary>
        /// this method change the data source to the browse data source
        /// </summary>
        /// <param name="dataSource">the data source for browsing</param>
        /// <param name="isBakeUp"></param>
        void Browse(string dataSource, bool isBakeUp);

        /// <summary>
        /// this method stop browsing the data base and return the data source ro the default data source
        /// </summary>
        void DisBrowse();
    }
}