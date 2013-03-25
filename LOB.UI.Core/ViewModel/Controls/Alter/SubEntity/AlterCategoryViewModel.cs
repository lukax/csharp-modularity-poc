﻿#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
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
            Entity = new Category { Name = "", Description = "", Code = 0 };
            _facade.SetEntity(Entity);
            _facade.ConfigureValidations();
        }

        public override void Refresh()
        {
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

        protected override bool CanSaveChanges(object arg)
        {
            //TODO: If viewState == Add : ..., If viewState == Update : ....
            IEnumerable<ValidationResult> results;
            return _facade.CanAdd(out results);
        }

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }

        public override OperationType OperationType
        {
            get { return OperationType.AlterCategory; }
        }
    }
}