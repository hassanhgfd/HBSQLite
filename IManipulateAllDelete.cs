using System;
using System.ComponentModel;

namespace HBSQLite
{
    /// <summary>
    /// provide methods for delete all in the table
    /// </summary>
    public interface IManipulateAllDelete
    {
        /// <summary>
        /// this Method Delete Data in the Table
        /// </summary>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        void Delete();
    }
}