using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using HBSQLite.Exception;

namespace HBSQLite
{
    /// <summary>
    /// provide methods and properties for work on the DataBase Faster and easier
    /// </summary>
    public class SQLiteDataBase : ISQLiteDataBase, IBackUp, IBrowse
    {
        private string _dataSource = "";

        #region Properties
        
        /// <summary>
        /// the Connection ReaderIndex
        /// </summary>
        internal SQLiteConnection Connection { get; set; }

        /// <summary>
        /// List Of <see cref="SQLiteTable" /> contain all DataBase Tables
        /// </summary>
        public List<SQLiteTable> Tables { get; } = new List<SQLiteTable>();

        /// <summary>
        /// the saveDataSource For this ReaderIndex
        /// </summary>
        public string DataSource { get; private set; }

        /// <summary>
        /// determine weather this in browsing mode or not
        /// </summary>
        public bool IsBrowsing => !string.IsNullOrWhiteSpace(_dataSource);

        /// <summary>
        /// determine weather this in Bake up database or not
        /// </summary>
        public bool IsBakeUp { get; private set; }

        #endregion

        #region constructors

        /// <summary>
        /// Create new Instance of <see cref="SQLiteDataBase" /> with the saveDataSource
        /// </summary>
        /// <param name="dataSource">the saveDataSource For Connection</param>
        public SQLiteDataBase(string dataSource)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
                throw new ArgumentException("message", nameof(dataSource));
            Initialize(dataSource, true);
        }

        #endregion

        /// <summary>
        /// Get the Column by it name
        /// </summary>
        /// <param name="tableName">the name of Table</param>
        /// <returns>the Table object</returns>
        /// <exception cref="SQLiteTableNotFoundException"></exception>
        public SQLiteTable this[string tableName]
        {
            get
            {
                return Tables?.Find((column) => column.Name.Equals(tableName))
                       ?? throw new SQLiteTableNotFoundException(tableName);
            }
        }

        /// <summary>
        /// Get the Column by it name
        /// </summary>
        /// <param name="tableName"><see cref="SQLiteTable"/> table object</param>
        /// <returns>the Table object</returns>
        /// <exception cref="SQLiteTableNotFoundException"></exception>
        public SQLiteTable this[SQLiteTable tableName]
        {
            get
            {
                return Tables?.Find((column) => column.Equals(tableName))
                       ?? throw new SQLiteTableNotFoundException(tableName);
            }
        }

        #region Methods

        private bool Exists(string dataSource) => File.Exists(dataSource);

        private void Initialize(string dataSource, bool canDiscover)
        {
            if (!Exists(dataSource)) throw new SQLiteDataSourceNotFoundException(dataSource);

            Connection?.Close();
            Connection = new SQLiteConnection($"Data Source={dataSource};Version=3;");
            Connection.OpenAsync();

            DataSource = dataSource;
            if (canDiscover)
                Tables.AddRange(Discover.DataBase(this));
        }

        #region IBrowse

        /// <summary>
        /// this method change the data source to the browse data source
        /// </summary>
        /// <param name="dataSource">the data source for browsing</param>
        /// <param name="isBakeUp">is it a bake up for the program database</param>
        public void Browse(string dataSource, bool isBakeUp)
        {
            if (string.IsNullOrWhiteSpace(dataSource))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(dataSource));
            _dataSource = DataSource;
            IsBakeUp = isBakeUp;
            Initialize(dataSource, !isBakeUp);
        }

        /// <summary>
        /// this method stop browsing the data base and return the data source ro the default data source
        /// </summary>
        public void DisBrowse()
        {
            Initialize(_dataSource, !IsBakeUp);
            _dataSource = "";
            IsBakeUp = false;
        }

        #endregion

        #region IBackUp

        /// <summary>
        /// this method for create bake up from current data source 
        /// </summary>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file</param>
        public void CreateBackUp(string bakeUpPath)
        {
            CreateBackUp(DataSource, bakeUpPath);
        }

        /// <summary>
        ///  this method for read the bake up and set it to the current data source
        /// </summary>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file</param>
        public void ReadBakeUp(string bakeUpPath)
        {
            ReadBakeUp(DataSource, bakeUpPath);
        }

        /// <summary>
        /// this method for create bake up from saveDataSource parameter 
        /// </summary>
        /// <param name="saveDataSource">the DataSource path for saving the bake up</param>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file</param>
        public void CreateBackUp(string saveDataSource, string bakeUpPath)
        {
            File.Copy(saveDataSource, bakeUpPath);
        }

        /// <summary>
        /// this method for read the bake up and set it to the saveDataSource parameter 
        /// </summary>
        /// <param name="saveDataSource">the DataSource path for saving the bake up</param>
        /// <param name="bakeUpPath">the bath for database that will be used as bake up file for reading</param>
        public void ReadBakeUp(string saveDataSource, string bakeUpPath)
        {
            Connection.Close();
            File.Delete(saveDataSource);
            File.Copy(bakeUpPath, saveDataSource);
            Connection.OpenAsync();
        }

        #endregion
        
        #endregion
    }
}
