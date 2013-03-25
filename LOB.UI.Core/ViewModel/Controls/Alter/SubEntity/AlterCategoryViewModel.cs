#region Usings

using System;
using System.Diagnostics;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterCategoryViewModel : AlterBaseEntityViewModel<Category>, IAlterCategoryViewModel
    {
        private readonly ICategoryFacade _facade;

        public AlterCategoryViewModel(Category entity, IRepository repository, ICategoryFacade facade)
            : base(entity, repository)
        {
            _facade = facade;
            Refresh();
        }

        public override void InitializeServices()
        {
            //_facade.GenerateEntity();
            //Entity = _facade.Entity;
        }

        public override void Refresh()
        {
        }

        public override OperationType OperationType
        {
            get { return OperationType.AlterCategory; }
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow.BeginTransaction())
            {
                Debug.Write("Saving changes...");
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}