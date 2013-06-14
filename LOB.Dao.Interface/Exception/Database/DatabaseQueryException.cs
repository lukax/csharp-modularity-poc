#region Usings

using LOB.Dao.Contract.Exception.Base;

#endregion

namespace LOB.Dao.Contract.Exception.Database {
    public class DatabaseQueryException : GenericDatabaseException {
        public DatabaseQueryException(string message = null, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}