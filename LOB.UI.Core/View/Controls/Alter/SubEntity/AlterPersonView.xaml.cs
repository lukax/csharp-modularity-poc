#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterPersonView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterPersonView()
        {
            InitializeComponent();
        }

        public AlterPersonViewModel<Person> ViewModel
        {
            set
            {
                this.DataContext = value;
                this.UcAlterAddressView.DataContext = value.AlterAddressViewModel;
                this.UcAlterContactInfoView.DataContext = value.AlterContactInfoViewModel;
            }
        }

        [ImportingConstructor]
        public AlterPersonView(AlterPersonViewModel<Person> viewModel)
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