#region Usings
using System.Windows.Controls;
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
    public partial class AlterEmployeeView : UserControl, IBaseView {

        public AlterEmployeeView() {
            this.InitializeComponent();
        }

        [Dependency] private IEventAggregator _eventAggregator { get; set; }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IAlterEmployeeViewModel; }
            set {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
                this.UcAlterNaturalPersonView.DataContext = value;
                var localViewModel = value as IAlterEmployeeViewModel;
                if(localViewModel != null) {
                    this.UcAlterNaturalPersonView.UcAlterPersonView.UcAlterAddressView.DataContext =
                        localViewModel.AlterAddressViewModel;
                    this.UcAlterNaturalPersonView.UcAlterPersonView.UcAlterContactInfoView.DataContext =
                        localViewModel.AlterContactInfoViewModel;
                }

                this._eventAggregator.GetEvent<SaveEvent>()
                    .Subscribe((s) => this._eventAggregator.GetEvent<CancelEvent>().Publish(null));
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Employee; }
        }

        public int Index {
            get { return ((AlterEmployeeViewModel) this.DataContext).Index; }
            set { ((AlterEmployeeViewModel) this.DataContext).Index = value; }
        }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterEmployee; }
        }

    }
}