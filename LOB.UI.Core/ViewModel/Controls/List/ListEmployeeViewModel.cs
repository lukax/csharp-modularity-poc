#region Usings

using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public class ListEmployeeViewModel : ListBaseEntityViewModel<Employee>
    {
        [ImportingConstructor]
        public ListEmployeeViewModel(Employee employee, IRepository repository) : base(employee, repository)
        {
        }

        public override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}