#region Usings

using System;
using System.Windows.Input;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
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
            : base(entity,repository)
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

        private void AddEmail(object arg)
        {
            _commandService["OpenView"].Execute(_container.Resolve<IAlterEmailViewModel>());
            _navigator.ResolveView("AlterEmail").Show();
        }

        private void DeleteEmail(object arg)
        {
        }

        private void AddPhoneNumber(object arg)
        {
            _commandService["OpenView"].Execute(_container.Resolve<IAlterPhoneNumberViewModel>());
            //Messenger.Default.Send<object>("AlterPhoneNumber", "OpenView");
        }

        private void DeletePhoneNumber(object arg)
        {
        }

        protected override void QuickSearch(object arg)
        {
            _commandService["QuickSearch"].Execute(_container.Resolve<ListContactInfoViewModel>());
            //Messenger.Default.Send<object>(_container.Resolve<ListContactInfoViewModel>(), "QuickSearchCommand");
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