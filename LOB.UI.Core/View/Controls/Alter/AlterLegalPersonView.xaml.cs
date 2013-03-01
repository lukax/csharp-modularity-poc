#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterLegalPersonView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterLegalPersonView()
        {
            InitializeComponent();
        }

        public AlterLegalPersonViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.TabAlterPersonView.DataContext = value;
                this.TabAlterPersonView.TabAlterAddressView.DataContext = value.AlterAddressViewModel;
                this.TabAlterPersonView.TabAlterContactInfoView.DataContext = value.AlterContactInfoViewModel;
            }
        }

        [ImportingConstructor]
        public AlterLegalPersonView(AlterLegalPersonViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
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