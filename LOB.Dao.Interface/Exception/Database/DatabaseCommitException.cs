using LOB.Dao.Contract.Exception.Base;

namespace LOB.Dao.Contract.Exception.Database
{
    public class DatabaseCommitException : GenericDatabaseException
    {
        public DatabaseCommitException(string message, string detail = null, System.Exception innerException = null)
            : base(message, detail, innerException) {}
    }
}
