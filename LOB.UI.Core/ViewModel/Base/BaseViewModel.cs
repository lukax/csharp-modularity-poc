#region Usings

using System;
using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.ViewModel.Base
{
    public abstract class BaseViewModel : BaseNotifyChange, IBaseViewModel
    {
        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        public abstract void InitializeServices();
        public abstract void Refresh();
        public abstract OperationType OperationType { get; }
    }
}