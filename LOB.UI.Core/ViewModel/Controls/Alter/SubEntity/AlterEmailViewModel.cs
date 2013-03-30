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
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {

        private readonly IEmailFacade _emailFacade;

        public AlterEmailViewModel(Email entity, IRepository repository, IEmailFacade emailFacade,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _emailFacade = emailFacade;
        }

        public override void InitializeServices() {
            Refresh();
        }

        public override void Refresh() {
            Entity = new Email {Value = "",};
            _emailFacade.SetEntity(Entity);
            _emailFacade.ConfigureValidations();
        }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow) {
                Repository.Uow.BeginTransaction();
                Repository.Save(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _emailFacade.CanAdd(out results);
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