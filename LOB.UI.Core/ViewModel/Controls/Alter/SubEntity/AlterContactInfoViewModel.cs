#region Usings

using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>
    {
        private IUnityContainer _container;

        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository, IUnityContainer container)
            : base(entity, repository)
        {
            _container = container;
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
            Messenger.Default.Send<object>("AlterEmail", "OpenView");
        }

        private void DeleteEmail(object arg)
        {
        }

        private void AddPhoneNumber(object arg)
        {
            Messenger.Default.Send<object>("AlterPhoneNumber", "OpenView");
        }

        private void DeletePhoneNumber(object arg)
        {
        }

        protected override void QuickSearch(object arg)
        {
            Messenger.Default.Send<object>(_container.Resolve<ListContactInfoViewModel>(), "QuickSearchCommand");
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