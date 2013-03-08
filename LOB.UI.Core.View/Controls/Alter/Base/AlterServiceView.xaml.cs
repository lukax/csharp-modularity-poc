#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.Base
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
        public AlterServiceView(AlterServiceViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public AlterServiceViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Service" : _header; }
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