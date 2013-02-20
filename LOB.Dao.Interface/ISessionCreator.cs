#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.Dao.Interface
{
    [InheritedExport]
    public interface ISessionCreator
    {
        object Orm { get; }
    }
}