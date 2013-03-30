#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter {
    public partial class AlterProductView : UserControl, IBaseView {

        private string _header;

        public AlterProductView() {
            this.InitializeComponent();
        }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IAlterProductViewModel; }
            set {
                this.DataContext = value;
                this.UcAlterBaseEntityView.DataContext = value;
                //Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Product; }
        }

        public int Index {
            get { return ((AlterBaseEntityViewModel<Product>) this.DataContext).Index; }
            set { ((AlterBaseEntityViewModel<Product>) this.DataContext).Index = value; }
        }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.AlterProduct; }
        }

    }
}