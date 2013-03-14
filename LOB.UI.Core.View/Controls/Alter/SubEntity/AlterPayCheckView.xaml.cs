#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    public partial class AlterPayCheckView : UserControl, IBaseView
    {
        private string _header;

        public AlterPayCheckView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterPayCheckViewModel; }
            set
            {
                DataContext = value;
                UcAlterBaseEntityView.DataContext = value;
            }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Paycheck" : _header; }
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