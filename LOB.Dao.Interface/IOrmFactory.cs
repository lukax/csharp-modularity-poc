#region Usings

using System;

#endregion

namespace LOB.Dao.Contract {
    public interface IOrmFactoryConfiguration : IDisposable {
        bool IsNewDatabase { get; }
        object OrmFactory { get; }
    }

    public interface IOrmFactory {
        object Orm { get; }
    }
}