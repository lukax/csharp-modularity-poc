#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterCategoryView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterCategoryView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterCategoryView(AlterCategoryViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public AlterCategoryViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                UcAlterServiceView.DataContext = value;
            }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Category" : _header; }
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