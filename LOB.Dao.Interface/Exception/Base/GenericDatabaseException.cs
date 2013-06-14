namespace LOB.Dao.Contract.Exception.Base {
    public class GenericDatabaseException : GenericDaoException {
        public GenericDatabaseException(string message, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}