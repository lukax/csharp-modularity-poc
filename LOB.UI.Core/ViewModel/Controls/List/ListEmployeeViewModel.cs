using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public class ListEmployeeViewModel : ListBaseEntityViewModel<Employee>
    {
        [ImportingConstructor]
        public ListEmployeeViewModel(Employee employee, IRepository repository) : base(employee, repository)
        {
        }

        public override void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}