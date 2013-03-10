#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base
{
    [Export]
    public partial class AlterPersonBaseView : UserControl, IBaseView
    {
        private string _header;

        public AlterPersonBaseView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterPersonBaseView(IAlterPersonViewModel<Person> viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set
            {
                DataContext = value;
                var localViewModel = value as IAlterPersonViewModel<Person>;
                if (localViewModel != null)
                {
                    UcAlterAddressView.DataContext = localViewModel.AlterAddressViewModel;
                    UcAlterContactInfoView.DataContext = localViewModel.AlterContactInfoViewModel;
                }
            }
        }


        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Pessoa" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}