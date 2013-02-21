#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterPersonViewModel : AlterBaseEntityViewModel<Person>
    {
        #region Props

        public string FirstName
        {
            get { return Entity.FirstName; }
            set
            {
                if (FirstName == value) return;
                Entity.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return Entity.LastName; }
            set
            {
                if (LastName == value) return;
                Entity.LastName = value;
                OnPropertyChanged();
            }
        }

        public string NickName
        {
            get { return Entity.NickName; }
            set
            {
                if (NickName == value) return;
                Entity.NickName = value;
                OnPropertyChanged();
            }
        }

        public DateTime BirthDate
        {
            get { return Entity.BirthDate; }
            set
            {
                if (BirthDate == value) return;
                Entity.BirthDate = value;
                OnPropertyChanged();
            }
        }

        public string Notes
        {
            get { return Entity.Notes; }
            set
            {
                if (Notes == value) return;
                Entity.Notes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private new Person Entity;

        [ImportingConstructor]
        public AlterPersonViewModel(Person entity, IRepository repository)
            : base(entity, repository)
        {
            Entity = entity;
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