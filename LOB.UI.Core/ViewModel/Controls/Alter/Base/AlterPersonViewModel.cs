#region Usings

using System;
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    [Export]
    public abstract class AlterPersonViewModel<T> : AlterBaseEntityViewModel<Person> where T:Person
    {
        public AlterAddressViewModel AlterAddressViewModel { get; set; }
        public AlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        [ImportingConstructor]
        public AlterPersonViewModel(Person entity, Address entityAddress, ContactInfo entityContactInfo, IRepository repository,
            AlterAddressViewModel alterAddressViewModel, AlterContactInfoViewModel alterContactInfoViewModel)
            : base(entity, repository)
        {
            AlterAddressViewModel = alterAddressViewModel;
            AlterContactInfoViewModel = alterContactInfoViewModel;

            Entity.Address = entityAddress;
            Entity.ContactInfo = entityContactInfo;

            AlterAddressViewModel.Entity = this.Entity.Address;
            AlterContactInfoViewModel.Entity = this.Entity.ContactInfo;
        }
        
        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override bool CanSaveChanges(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg)
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