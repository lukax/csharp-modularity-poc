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

        public AlterProductView() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IAlterProductViewModel; }
            set {
                DataContext = value;
                ViewEditTools.DataContext = value;
                ViewAlterBaseEntity.DataContext = value;
                ViewConfCancelTools.DataContext = value;
                //Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
            }
        }

        public string Header {
            get { return Strings.Header_Alter_Product; }
        }

        public int Index {
            get { return ((AlterBaseEntityViewModel<Product>)DataContext).Index; }
            set { ((AlterBaseEntityViewModel<Product>)DataContext).Index = value; }
        }

        public void InitializeServices() { }

        public void Refresh() { }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }

    }
}