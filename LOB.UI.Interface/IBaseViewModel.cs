#region Usings

using System.ComponentModel;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface
{
    public interface IBaseViewModel : IUIComponent//, IDataErrorInfo
    {
        void InitializeServices();
        void Refresh();
    }
}