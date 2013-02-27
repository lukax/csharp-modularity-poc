#region Usings

using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>
    {
        #region Props

        public string Street
        {
            get { return Entity.Street; }
            set
            {
                Entity.Street = value;
                OnPropertyChanged();
            }
        }

        public string Complement
        {
            get { return Entity.StreetComplement; }
            set
            {
                Entity.StreetComplement = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get { return Entity.City; }
            set
            {
                Entity.City = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get { return Entity.City; }
            set
            {
                Entity.City = value;
                OnPropertyChanged();
            }
        }

        public AdressStatus Status
        {
            get { return Entity.Status; }
            set
            {
                Entity.Status = value;
                OnPropertyChanged();
            }
        }

        public int StreetNumber
        {
            get { return Entity.StreetNumber; }
            set
            {
                Entity.StreetNumber = value;
                OnPropertyChanged();
            }
        }

        public string District
        {
            get { return Entity.District; }
            set
            {
                Entity.District = value;
                OnPropertyChanged();
            }
        }

        public string State
        {
            get { return Entity.State; }
            set
            {
                Entity.State = value;
                OnPropertyChanged();
            }
        }

        public int Zip
        {
            get { return Entity.ZipCode; }
            set
            {
                Entity.ZipCode = value;
                OnPropertyChanged();
            }
        }

        public bool Default
        {
            get { return Entity.IsDefault; }
            set
            {
                Entity.IsDefault = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public AlterAddressViewModel(Address entity, IRepository repository)
            : base(entity, repository)
        {
        }

        public override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
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