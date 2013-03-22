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

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterEmployeeView : UserControl, IBaseView
    {
        [Dependency]
        private IEventAggregator _eventAggregator { get; set; }

        public AlterEmployeeView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterEmployeeViewModel; }
            set
            {
                DataContext = value;
                UcAlterBaseEntityView.DataContext = value;
                UcAlterNaturalPersonView.DataContext = value;
                var localViewModel = value as IAlterEmployeeViewModel;
                if (localViewModel != null)
                {
                    UcAlterNaturalPersonView.UcAlterPersonView.UcAlterAddressView.DataContext =
                        localViewModel.AlterAddressViewModel;
                    UcAlterNaturalPersonView.UcAlterPersonView.UcAlterContactInfoView.DataContext =
                        localViewModel.AlterContactInfoViewModel;
                }

                _eventAggregator.GetEvent<SaveEvent>()
                                .Subscribe((s) => _eventAggregator.GetEvent<CancelEvent>().Publish(null));
            }
        }


        public string Header { get { return Strings.Header_Alter_Employee; } }

        public int? Index
        {
            get { return ((AlterEmployeeViewModel) DataContext).CancelIndex; }
            set { ((AlterEmployeeViewModel) DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public OperationType OperationType
        {
            get { return OperationType.AlterEmployee; }
        }
    }
}