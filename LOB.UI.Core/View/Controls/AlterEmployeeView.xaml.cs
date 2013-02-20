#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls;
using LOB.UI.Core.ViewModel.Controls.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls
{
    [Export]
    public partial class AlterEmployeeView : UserControl, ITabProp, IView
    {
        [ImportingConstructor]
        public AlterEmployeeView(AlterEntityViewModel<Employee> dataContext)
        {
            InitializeComponent();
            dataContext.Entity = new Employee();
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
            get { return ((AlterEntityViewModel<Employee>) DataContext).CancelIndex; }
            set { ((AlterEntityViewModel<Employee>) DataContext).CancelIndex = value; }
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