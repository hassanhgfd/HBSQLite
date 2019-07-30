namespace HBSQLite
{
    /// <summary>
    /// provide methods for create and read bake up
    /// </summary>
    public interface IBackUp
    {
        /// <summary>
        /// this method for create bake up from current data source 
        /// </summary>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file</param>
        void CreateBackUp(string bakeUpPath);

        /// <summary>
        ///  this method for read the bake up and set it to the current data source
        /// </summary>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file</param>
        void ReadBakeUp(string bakeUpPath);

        /// <summary>
        /// this method for create bake up from saveDataSource parameter 
        /// </summary>
        /// <param name="saveDataSource">the DataSource path for saving the bake up</param>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file</param>
        void CreateBackUp(string saveDataSource, string bakeUpPath);

        /// <summary>
        /// this method for read the bake up and set it to the saveDataSource parameter 
        /// </summary>
        /// <param name="saveDataSource">the DataSource path for Saving the bake up</param>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file for reading</param>
        void ReadBakeUp(string saveDataSource, string bakeUpPath);
    }
}