#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>,
                                                IAlterAddressViewModel {

        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly IAddressFacade _addressFacade;
        private readonly IEventAggregator _eventAggregator;
        private string _status;
        private IList<string> _statuses;
        public ObservableCollection<UF> UFs { get; set; }
        public ObservableCollection<string> Districts { get; set; } 
        private UF _uf;
        public UF UF {
            get { return _uf; }
            set {
                _uf = value;
                Entity.State = value.ToLocalizedString();
                Districts = new ObservableCollection<string>(value.GetDistricts());
            }
        }
        [AllowNull]
        public string Status {
            get { return _status; }
            set {
                _status = value;
                Entity.Status = AddressStatusDictionary.Statuses[value];
            }
        }

        [InjectionConstructor]
        public AlterAddressViewModel(Address entity, IRepository repository,
            IAddressFacade addressFacade, IEventAggregator eventAggregator,
            ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _addressFacade = addressFacade;
            _eventAggregator = eventAggregator;
        }

        public IList<string> Statuses {
            get {
                if(_statuses != null) return _statuses;
                _statuses = new List<string>(AddressStatusDictionary.Statuses.Keys);
                Status = _statuses.FirstOrDefault();
                return _statuses;
            }
        }

        public override void InitializeServices() {
            ClearEntity(null);
            Operation = _operation;
            _worker.DoWork += UpdateUFList;
            _worker.RunWorkerAsync();
            _eventAggregator.GetEvent<IncludeEvent>().Subscribe(Include);
            _eventAggregator.GetEvent<NotificationEvent>().Publish(new Notification{ Message = string.Format("{0} {1}", Strings.Common_Initialized, Strings.Command_Alter_Address), Progress = 0, Severity = Severity.Info});
        }

        private void Include(BaseEntity obj) {
            var entity = obj as Address;
            if(entity == null) return;
            Entity = entity;
            Operation.State = UIOperationState.Update;
        }

        private void UpdateUFList(object sender, DoWorkEventArgs doWorkEventArgs) { UFs = new ObservableCollection<UF>(Enum.GetValues(typeof(UF)).Cast<UF>()); }

        public override void Refresh() { ClearEntity(null); }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(Operation.State == UIOperationState.Add) return _addressFacade.CanAdd(out results);
            if(Operation.State == UIOperationState.Update) return _addressFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            if(Operation.State == UIOperationState.Add) return true;
            if(Operation.State == UIOperationState.Update) return true;
            return false;
        }

        protected override void ClearEntity(object arg) {
            Entity = _addressFacade.GenerateEntity();
            UF = Entity.State.ToUF();
            _addressFacade.SetEntity(Entity);
            _addressFacade.ConfigureValidations();
        }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Address,
            State = UIOperationState.Add
        };

    }
}