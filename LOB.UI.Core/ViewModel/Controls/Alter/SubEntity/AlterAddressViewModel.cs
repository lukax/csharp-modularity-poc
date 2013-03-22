#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
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
            get { return Entity.State ?? (Entity.State = string.Empty); }
            set
            {
                if (value.Length == 2)
                {
                    try
                    {
                        UfBr parsed;
                        if(Enum.TryParse(value, out parsed))
                            Entity.State = UfBrDictionary.Ufs[parsed];
                    }
                    catch (ArgumentNullException)
                    {
                        Entity.State = value;
                    }
                }
            }
        }

        public override OperationType OperationType
        {
            get { return OperationType.NewAddress; }
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
            //_commandService.Execute("QuickSearch", OperationName.ListAddress);
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}