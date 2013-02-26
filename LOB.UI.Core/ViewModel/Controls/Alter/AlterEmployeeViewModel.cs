#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>
    {
        #region Props

        public string Title {
            get { return Entity.Title; }
            set {
                if (Entity.Title == value) return;
                Entity.Title = value;
                OnPropertyChanged();
            }
        }

        public string HireDate {
            get {
                return Entity.HireDate == default(DateTime)
                           ? DateTime.Now.ToShortDateString()
                           : Entity.HireDate.ToShortDateString();
            }
            set {
                var backup = Entity.HireDate;
                try {
                    if (HireDate == value) return;
                    Entity.HireDate = DateTime.Parse(value);
                    OnPropertyChanged();
                }
                catch (FormatException) {
                    Entity.HireDate = backup;
                }
            }
        }

        #endregion

        [ImportingConstructor]
        public AlterEmployeeViewModel(Employee employee, IRepository repository)
            : base(employee, repository) {
        }

        public override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        public override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        public override void InitializeServices() {
        }

        public override void Refresh() {
        }
    }
}