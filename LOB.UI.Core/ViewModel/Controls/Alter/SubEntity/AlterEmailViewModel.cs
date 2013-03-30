#region Usings
using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {

        private readonly IEmailFacade _emailFacade;

        public AlterEmailViewModel(Email entity, IRepository repository, IEmailFacade emailFacade)
            : base(entity, repository) {
            this._emailFacade = emailFacade;
        }

        public override void InitializeServices() {
            this.Refresh();
        }

        public override void Refresh() {
            this.Entity = new Email {Value = "",};
            this._emailFacade.SetEntity(this.Entity);
            this._emailFacade.ConfigureValidations();
        }

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow) {
                this.Repository.Uow.BeginTransaction();
                this.Repository.Save(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return this._emailFacade.CanAdd(out results);
        }

        protected override void QuickSearch(object arg) {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg) {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterEmail; }
        }

    }
}