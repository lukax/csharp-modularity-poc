#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterProductViewModel : AlterBaseEntityViewModel<Product>
    {
        private IUnityContainer _container;

        [ImportingConstructor]
        public AlterProductViewModel(Product product, IRepository repository, IUnityContainer container)
            : base(product, repository)
        {
            _container = container;
            Categories = Repository.GetList<Category>().ToList();
        }

        public IList<Category> Categories { get; set; }

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