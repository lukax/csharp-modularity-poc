#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterContactInfoView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterContactInfoView()
        {
            InitializeComponent();
        }

        public AlterContactInfoViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
            }
        }

        [ImportingConstructor]
        public AlterContactInfoView(AlterContactInfoViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
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