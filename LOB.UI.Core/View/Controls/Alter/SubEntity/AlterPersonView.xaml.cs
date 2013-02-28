#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
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

        [ImportingConstructor]
        public AlterPersonView(AlterPersonViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
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