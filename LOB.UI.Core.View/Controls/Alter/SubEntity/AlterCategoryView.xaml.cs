#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    public partial class AlterCategoryView : UserControl, IBaseView
    {
        private string _header;

        public AlterCategoryView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterCategoryViewModel; }
            set
            {
                DataContext = value;
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

        public Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.AlterCategory; }
        }
    }
}