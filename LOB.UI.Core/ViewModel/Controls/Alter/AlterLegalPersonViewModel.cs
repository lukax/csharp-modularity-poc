#region Usings
using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterLegalPersonViewModel : AlterBaseEntityViewModel<LegalPerson>, IAlterLegalPersonViewModel {

        private IUnityContainer _container;

        [InjectionConstructor] public AlterLegalPersonViewModel(LegalPerson entity, IRepository repository)
            : base(entity, repository) {}

        public override void InitializeServices() {
            throw new NotImplementedException();
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterLegalPerson; }
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        protected override void SaveChanges(object arg) {
            using(this.Repository.Uow) {
                this.Repository.Uow.BeginTransaction();
                this.Repository.SaveOrUpdate(this.Entity);
                this.Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg) {
            //Messenger.Default.Send<object>(_container.Resolve<ListLegalPersonViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg) {
            this.Entity = new LegalPerson();
        }

    }
}