#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List.Base
{
    [Export]
    public partial class ListServiceView : UserControl, IView, ITabProp
    {
        private string _header;

        public ListServiceView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListServiceView(ListServiceViewModel<Service> viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public ListServiceViewModel<Service> ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Services" : _header; }
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