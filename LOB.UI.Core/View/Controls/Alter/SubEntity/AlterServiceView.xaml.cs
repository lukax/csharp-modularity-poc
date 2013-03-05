#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterServiceView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterServiceView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterServiceView(AlterBaseEntityViewModel<Service> viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public AlterBaseEntityViewModel<Service> ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Clientes" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}