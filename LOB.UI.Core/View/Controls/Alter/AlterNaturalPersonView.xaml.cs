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
    public partial class AlterNaturalPersonView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterNaturalPersonView()
        {
            InitializeComponent();
        }

        public AlterNaturalPersonViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.TabAlterPersonView.DataContext = value;
            }
        }
        [ImportingConstructor]
        public AlterNaturalPersonView(AlterNaturalPersonViewModel viewModel)
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