#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
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
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>, IAlterAddressViewModel {

        private readonly IAddressFacade _addressFacade;
        private readonly IEventAggregator _eventAggregator;
        private string _status;
        private IList<string> _statuses;

        public AlterAddressViewModel(Address entity, IRepository repository, IAddressFacade addressFacade,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _addressFacade = addressFacade;
            _eventAggregator = eventAggregator;
        }

        //TODO: Wrap with business logic
        public string State {
            get { return Entity.State; }
            set {
                if(value.Length == 2)
                    try {
                        UfBr parsed;
                        if(Enum.TryParse(value, out parsed)) Entity.State = UfBrDictionary.Ufs[parsed];
                    }
                    catch(ArgumentNullException) {
                        Entity.State = value;
                    }
            }
        }

        [AllowNull] public string Status {
            get { return _status; }
            set {
                _status = value;
                Entity.Status = AddressStatusDictionary.Statuses[value];
            }
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
        }

        public override void Refresh() {
            ClearEntity(null);
        }

        public override OperationType OperationType {
            get { return OperationType.AlterAddress; }
        }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow) {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) {
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(OperationType);
        }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _addressFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg) {
            _eventAggregator.GetEvent<QuickSearchEvent>().Publish(OperationType);
        }

        protected override void ClearEntity(object arg) {
            Entity = new Address
            {
                Code = 0,
                District = "",
                City = "Nova Friburgo",
                Country = "Brasil",
                State = "Rio de Janeiro",
                ZipCode = 0,
                Street = "",
                StreetNumber = 0,
                StreetComplement = "",
                IsDefault = false,
                Status = default(AddressStatus),
            };
            _addressFacade.SetEntity(Entity);
            _addressFacade.ConfigureValidations();
        }

    }
}