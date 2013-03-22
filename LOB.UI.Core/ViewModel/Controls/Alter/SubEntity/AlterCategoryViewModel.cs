#region Usings

using System;
using System.Diagnostics;
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
    public class AlterCategoryViewModel : AlterServiceViewModel<Category>, IAlterCategoryViewModel
    {
        private ICommandService _commandService;
        private IUnityContainer _container;

        public AlterCategoryViewModel(Category entity, IRepository repository, IUnityContainer container,
                                      ICommandService commandService) : base(entity, repository)
        {
            _commandService = commandService;
            _container = container;
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