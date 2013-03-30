#region Usings
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

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterCategoryViewModel : AlterBaseEntityViewModel<Category>, IAlterCategoryViewModel {

        private readonly ICategoryFacade _facade;

        public AlterCategoryViewModel(Category entity, IRepository repository, ICategoryFacade facade)
            : base(entity, repository) {
            this._facade = facade;
            this.Refresh();
        }

        public override void InitializeServices() {
            this.Entity = new Category {Name = "", Description = "", Code = 0};
            this._facade.SetEntity(this.Entity);
            this._facade.ConfigureValidations();
        }

        public override void Refresh() {}

        public override OperationType OperationType {
            get { return OperationType.AlterCategory; }
        }

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow.BeginTransaction()) {
                Debug.Write("Saving changes...");
                this.Entity = this.Repository.SaveOrUpdate(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: If viewState == Add : ..., If viewState == Update : ....
            IEnumerable<ValidationResult> results;
            return this._facade.CanAdd(out results);
        }

        protected override void QuickSearch(object arg) {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg) {
            throw new NotImplementedException();
        }

    }
}