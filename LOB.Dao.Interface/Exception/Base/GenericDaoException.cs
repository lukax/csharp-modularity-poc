#region Usings

using LOB.Util.Contract.Exception;

#endregion

namespace LOB.Dao.Contract.Exception.Base {
    public class GenericDaoException : BaseException {
        public GenericDaoException(string message, string detail = null, System.Exception innerException = null)
                : base(message, detail, innerException) { }
    }
}