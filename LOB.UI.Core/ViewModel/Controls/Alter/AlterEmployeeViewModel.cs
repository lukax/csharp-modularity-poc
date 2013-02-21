using System;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>
    {
        #region Props
        public string Title
        {
            get { return Entity.Title; }
            set
            {
                if (Title == value) return;
                Entity.Title = value;
                OnPropertyChanged();
            }
        }
        public DateTime HireDate
        {
            get { return Entity.HireDate; }
            set
            {
                if (HireDate == value) return;
                Entity.HireDate = value;
                OnPropertyChanged();
            }
        }
        #endregion

        [ImportingConstructor]
        public AlterEmployeeViewModel(Employee employee, IRepository repository)
            : base(employee, repository)
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