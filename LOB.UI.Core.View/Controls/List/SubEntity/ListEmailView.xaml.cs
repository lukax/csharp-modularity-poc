#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    [Export]
    public partial class ListEmailView : UserControl, IView
    {
        private string _header;

        public ListEmailView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListEmailView(ListEmailViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public ListEmailViewModel ViewModel
        {
            set { this.DataContext = value; }
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