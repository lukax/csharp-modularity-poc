#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Core.Events;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterEmployeeView : IBaseView {

        public AlterEmployeeView() { InitializeComponent(); }

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterEmployeeViewModel; }
            set {
                DataContext = value;
                ViewEditTools.DataContext = value;
                ViewAlterBaseEntity.DataContext = value;
                ViewAlterNaturalPerson.DataContext = value;
                var localViewModel = value as IAlterEmployeeViewModel;
                if(localViewModel != null) {
                    ViewAlterNaturalPerson.ViewAlterPerson.ViewAlterAddress.DataContext =
                        localViewModel.AlterAddressViewModel;
                    ViewAlterNaturalPerson.ViewAlterPerson.ViewAlterContactInfo.DataContext =
                        localViewModel.AlterContactInfoViewModel;
                }

                EventAggregator.GetEvent<SaveEvent>()
                               .Subscribe(s => EventAggregator.GetEvent<CancelEvent>().Publish(null));
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Employee; }
        }

        public int Index {
            get { return ((AlterEmployeeViewModel)DataContext).Index; }
            set { ((AlterEmployeeViewModel)DataContext).Index = value; }
        }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}