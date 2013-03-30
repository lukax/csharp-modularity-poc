#region Usings

using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.ViewModel.Base {
    public abstract class BaseViewModel : BaseNotifyChange, IBaseViewModel {

        public string Header { get; set; }
        public abstract void InitializeServices();
        public abstract void Refresh();
        public abstract OperationType OperationType { get; }

    }
}