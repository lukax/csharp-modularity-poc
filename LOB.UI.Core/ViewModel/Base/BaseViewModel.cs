#region Usings

using System;
using LOB.Domain.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.ViewModel.Base
{
    public abstract class BaseViewModel : BaseNotifyChange, IView
    {
        private String _title;

        public String Title {
            get { return _title; }
            set {
                _title = value;
                OnPropertyChanged();
            }
        }

        public abstract void InitializeServices();
        public abstract void Refresh();
    }
}