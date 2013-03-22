#region Usings

using System.Windows.Controls;
using LOB.UI.Core.Events;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterEmployeeView : UserControl, IBaseView
    {
        private readonly IEventAggregator _eventAggregator;
        private string _header;

        public AlterEmployeeView(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
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


        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Funcionário" : _header; }
            set { _header = value; }
        }

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
            get { return OperationType.NewEmployee; }
        }
    }
}