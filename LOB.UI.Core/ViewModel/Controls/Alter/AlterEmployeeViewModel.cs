using System;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>
    {
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
        
        public AlterEmployeeViewModel()
            : base(new Employee())
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