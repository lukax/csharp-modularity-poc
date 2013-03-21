#region Usings

using System;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel
    {
        private IUnityContainer _container;

        public AlterPayCheckViewModel(PayCheck entity, IRepository repository, IUnityContainer container)
            : base(entity, repository)
        {
            _container = container;
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
            Messenger.Default.Send<object>(_container.Resolve<ListPayCheckViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }

        public override Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.NewPayCheck; }
        }
    }
}