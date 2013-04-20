#region Usings

using System.Globalization;
using System.Threading;
using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.ViewModel.Base {
    public abstract class BaseViewModel : BaseNotifyChange, IBaseViewModel {
        public abstract ViewModelState ViewModelState { get; set; }
        public string Header { get; set; }
        public ViewModelState State { get; set; }
        public abstract void InitializeServices();
        public abstract void Refresh();

        public static CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public abstract void Dispose();
    }
}