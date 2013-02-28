#region Usings

using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public sealed class ListEmployeeViewModel : ListBaseEntityViewModel<Employee>
    {

        [ImportingConstructor]
        public ListEmployeeViewModel(Employee employee, IRepository repository)
            : base(employee, repository)
        {
        }

        protected override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}