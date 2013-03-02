#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Base;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    [Export]
    public partial class ListBaseEntityView : UserControl, IView, ITabProp
    {
        private string _header;

        public ListBaseEntityView()
        {
            InitializeComponent();
        }

        public BaseViewModel ViewModel { set { this.DataContext = value; } }
        [ImportingConstructor]
        public ListBaseEntityView(BaseViewModel viewModel)
            : this()
        {

            ViewModel = viewModel;
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Códigos" : _header; }
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