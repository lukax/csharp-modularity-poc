#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
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

        public string BirthDate
        {
            get { return (Entity.BirthDate == default(DateTime) ? DateTime.Now : Entity.BirthDate).ToShortDateString(); }
            set
            {
                string backup = string.Empty;
                try {
                    if (BirthDate == value) return;
                    backup = BirthDate;
                    Entity.BirthDate = DateTime.Parse(value);
                    OnPropertyChanged();
                }
                catch (FormatException) {
                    BirthDate = backup;
                }
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

        protected AlterAddressViewModel AlterAddressViewModel;
        protected AlterContactInfoViewModel AlterContactInfoViewModel;

        [ImportingConstructor]
        public AlterPersonViewModel(Person entity, IRepository repository,
                                    AlterAddressViewModel alterAdressViewModel,
                                    AlterContactInfoViewModel alterContactInfoViewModel)
            : base(entity, repository)
        {
            AlterAddressViewModel = alterAdressViewModel;
            AlterContactInfoViewModel = alterContactInfoViewModel;
        }

        public override void SaveChanges(object arg)
        {
            using (Repository.Uow) {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
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