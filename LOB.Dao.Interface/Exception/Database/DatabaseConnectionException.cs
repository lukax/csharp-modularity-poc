#region Usings

using LOB.Dao.Contract.Exception.Base;

#endregion

namespace LOB.Dao.Contract.Exception.Database {
    public class DatabaseConnectionException : GenericDaoException {
        public DatabaseConnectionException(string message = null, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}