#region Usings

using System;
using System.Windows.Input;
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
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>, IAlterContactInfoViewModel
    {
        private ICommandService _commandService;
        private IUnityContainer _container;
        private IFluentNavigator _navigator;

        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository, IUnityContainer container,
                                         ICommandService commandService, IFluentNavigator navigator)
            : base(entity, repository)
        {
            _container = container;
            _commandService = commandService;
            _navigator = navigator;
            Entity = entity;
            AddEmailCommand = new DelegateCommand(AddEmail);
            DeleteEmailCommand = new DelegateCommand(DeleteEmail);
            AddPhoneNumberCommand = new DelegateCommand(AddPhoneNumber);
            DeletePhoneNumberCommand = new DelegateCommand(DeletePhoneNumber);
        }

        public ICommand AddEmailCommand { get; set; }
        public ICommand DeleteEmailCommand { get; set; }
        public ICommand AddPhoneNumberCommand { get; set; }
        public ICommand DeletePhoneNumberCommand { get; set; }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
            Entity = new ContactInfo();
        }

        public override OperationType OperationType
        {
            get { return OperationType.AlterContactInfo; }
        }

        private void AddEmail(object arg)
        {
            //_commandService.Execute("OpenView", OperationName.AlterEmail);
        }

        private void DeleteEmail(object arg)
        {
        }

        private void AddPhoneNumber(object arg)
        {
            //_commandService.Execute("OpenView", OperationName.AlterPhoneNumber);
        }

        private void DeletePhoneNumber(object arg)
        {
        }

        protected override void QuickSearch(object arg)
        {
            //_commandService.Execute("OpenView", OperationName.ListContactInfo);
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
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
    }
}