#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Unity;
using LOB.UI.Core.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>
    {
        private IUnityContainer _container;
        public IList<Category> Categories { get; set; }

        [ImportingConstructor]
        public AlterProductViewModel(Product product, IRepository repository, IUnityContainer container)
            : base(product, repository)
        {
            _container = container;
            Categories = Repository.GetList<Category>().ToList();
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }

            Messenger.Default.Send("SaveChangesCommand");
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
            Messenger.Default.Send<object>(_container.Resolve<ListProductViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object args)
        {
            Entity = new Product();
        }

    }
}