#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    [Export]
    public partial class ListBaseEntityView : UserControl, IView, ITabProp
    {
        private string _header;

        public ListBaseEntityView() {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListBaseEntityView(ListBaseEntityViewModel<BaseEntity> viewModel)
            : this() {
            DataContext = viewModel;
        }

        public string Header {
            get { return (string.IsNullOrEmpty(_header)) ? "Códigos" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices() {
        }

        public void Refresh() {
        }
    }
}