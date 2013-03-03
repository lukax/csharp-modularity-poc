#region Usings

using System;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
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