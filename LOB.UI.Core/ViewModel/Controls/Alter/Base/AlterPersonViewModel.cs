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
    public abstract class AlterPersonViewModel<T> : AlterBaseEntityViewModel<Person> where T:Person
    {
        public new T Entity { get { return base.Entity as T; } set { base.Entity = value; } }

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