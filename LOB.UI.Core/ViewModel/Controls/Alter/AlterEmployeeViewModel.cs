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

        public override bool CanSaveChanges(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanCancel(object arg)
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