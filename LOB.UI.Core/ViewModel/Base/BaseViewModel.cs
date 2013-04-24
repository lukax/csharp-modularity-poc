#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.ViewModel.Base {
    public abstract class BaseViewModel : BaseNotifyChange, IBaseViewModel, IEquatable<BaseViewModel> {
        [Import("ViewId")] public Guid Id { get; private set; }
        public virtual string Header {
            get { return Strings.Common_Title; }
        }
        protected static CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }
        protected BackgroundWorker Worker { get; private set; }
        protected BaseViewModel() { Worker = new BackgroundWorker(); }
        public abstract ViewModelInfo Info { get; set; }
        public abstract void InitializeServices();
        public abstract void Refresh();
        public abstract void Dispose();
        public bool Equals(BaseViewModel other) { return Id == other.Id; }
    }

    [PartCreationPolicy(CreationPolicy.NonShared)] //INFO: Makes property Id not shared across views
    internal class BaseViewModelHelper {
        [Export("ViewId")] public Guid Id {
            get { return Guid.NewGuid(); }
        }

        [Export] public BackgroundWorker Worker {
            get { return new BackgroundWorker(); }
        }
    }
}