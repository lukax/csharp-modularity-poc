#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterEmployeeView : UserControl, ITabProp, IView
    {
        [ImportingConstructor]
        public AlterEmployeeView(AlterEmployeeViewModel dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            //Registrations
            Messenger.Default.Register<object>(dataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        public string Header
        {
            get { return "Alterar Funcionario"; }
            set { }
        }

        public int? Index
        {
            get { return ((AlterBaseEntityViewModel<Employee>) DataContext).CancelIndex; }
            set { ((AlterBaseEntityViewModel<Employee>) DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}