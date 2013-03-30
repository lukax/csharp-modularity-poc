#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>, IAlterAddressViewModel {

        private readonly IAddressFacade _addressFacade;
        private string _status;
        private IList<string> _statuses;

        public AlterAddressViewModel(Address entity, IRepository repository, IAddressFacade addressFacade)
            : base(entity, repository) {
            this._addressFacade = addressFacade;
        }

        //TODO: Wrap with business logic
        public string State {
            get { return this.Entity.State; }
            set {
                if(value.Length == 2)
                    try {
                        UfBr parsed;
                        if(Enum.TryParse(value, out parsed)) this.Entity.State = UfBrDictionary.Ufs[parsed];
                    }
                    catch(ArgumentNullException) {
                        this.Entity.State = value;
                    }
            }
        }

        [AllowNull] public string Status {
            get { return this._status; }
            set {
                this._status = value;
                this.Entity.Status = AddressStatusDictionary.Statuses[value];
            }
        }

        public IList<string> Statuses {
            get {
                if(this._statuses != null) return this._statuses;
                this._statuses = new List<string>(AddressStatusDictionary.Statuses.Keys);
                this.Status = this._statuses.FirstOrDefault();
                return this._statuses;
            }
        }

        public override void InitializeServices() {
            this.Refresh();
        }

        public override void Refresh() {
            this.Entity = new Address {
                District = "",
                City = "Nova Friburgo",
                Country = "Brasil",
                State = "Rio de Janeiro",
                ZipCode = 123456789,
                Street = "",
                StreetComplement = "",
                IsDefault = false,
            };
            this._addressFacade.SetEntity(this.Entity);
            this._addressFacade.ConfigureValidations();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterAddress; }
        }

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow) {
                this.Repository.Uow.BeginTransaction();
                this.Repository.SaveOrUpdate(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            IEnumerable<ValidationResult> results;
            return this._addressFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg) {
            //_commandService.Execute("QuickSearch", OperationName.ListAddress);
        }

        protected override void ClearEntity(object arg) {
            throw new NotImplementedException();
        }

    }
}