#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    [Export]
    public partial class ListProductView : UserControl, ITabProp, IView
    {
        private string _header;

        public ListProductView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListProductView(ListProductViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public ListProductViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Produtos" : _header; }
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