#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>, IAlterAddressViewModel
    {
        private ICommandService _commandService;
        private IUnityContainer _container;

        public AlterAddressViewModel(Address entity, IRepository repository, IUnityContainer container,
                                     ICommandService commandService)
            : base(entity, repository)
        {
            _commandService = commandService;
            _container = container;
        }

        //TODO: Wrap with business logic
        public string State
        {
            get { return Entity.State; }
            set
            {
                if (value.Length == 2)
                {
                    try
                    {
                        UfBr parsed;
                        Enum.TryParse(value, out parsed);
                        Entity.State = UfBrDictionary.Ufs[parsed];
                    }
                    catch (ArgumentNullException)
                    {
                        Entity.State = value;
                    }
                }
            }
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

        protected override void QuickSearch(object arg)
        {
            _commandService["QuickSearch"].Execute(_container.Resolve<ListAddressViewModel>());
            //Messenger.Default.ExecuteCommand<object>(_container.Resolve<ListAddressViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}