using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListEmployeeViewModel : ListBaseEntityViewModel<Employee>
    {
        public ListEmployeeViewModel() : base(new Employee())
        {
            Entity = new Employee();
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