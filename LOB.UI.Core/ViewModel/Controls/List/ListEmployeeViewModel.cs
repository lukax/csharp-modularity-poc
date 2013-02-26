#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public class ListEmployeeViewModel : ListBaseEntityViewModel<Employee>
    {
        #region Props

        private Lazy<IQueryable<Employee>> _employees;

        public IList<Employee> Employees {
            get { return _employees.Value.ToList(); }
        }

        public Employee Employee {
            get { return Entity; }
            set {
                if (Entity == value) return;
                Entity = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [ImportingConstructor]
        public ListEmployeeViewModel(Employee employee, IRepository repository, Person person)
            : base(employee, repository) {
            if (person != null)
                Employee.Person = person;

            _employees = new Lazy<IQueryable<Employee>>(Repository.GetList<Employee>);
        }

        public override bool CanUpdate(object arg) {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg) {
            //TODO: Business logic
            return true;
        }

        public override void InitializeServices() {
        }

        public override void Refresh() {
        }
    }
}