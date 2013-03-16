#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public sealed class AlterEmployeeViewModel : AlterNaturalPersonViewModel, IAlterEmployeeViewModel
    {
        private ICommandService _commandService;
        private IUnityContainer _container;

        [InjectionConstructor]
        public AlterEmployeeViewModel(Employee entity, Address address, ContactInfo contactInfo,
                                      IRepository repository, AlterAddressViewModel alterAddressViewModel,
                                      AlterContactInfoViewModel alterContactInfoViewModel, IUnityContainer container,
                                      ICommandService commandService)
            : base(entity, address, contactInfo, repository, alterAddressViewModel, alterContactInfoViewModel, container
                )
        {
            _container = container;
            _commandService = commandService;
            Entity = entity;
        }

        public new Employee Entity { get; set; }

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
            _commandService.Execute("QuickSearch", OperationName.ListEmployee);
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}