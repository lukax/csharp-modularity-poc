#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {
        private readonly IEmailFacade _emailFacade;
        private readonly IEventAggregator _eventAggregator;

        public AlterEmailViewModel(Email entity, IRepository repository, IEmailFacade emailFacade,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _emailFacade = emailFacade;
            _eventAggregator = eventAggregator;
        }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Repository.Save(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _emailFacade.CanAdd(out results);
        }

        protected override void ClearEntity(object arg) {
            Entity = new Email {Value = "", Code = 0};
            _emailFacade.SetEntity(Entity);
            _emailFacade.ConfigureValidations();
        }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Email,
            State = UIOperationState.Add
        };
    }
}