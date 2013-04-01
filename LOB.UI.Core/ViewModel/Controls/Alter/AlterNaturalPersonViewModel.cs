#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public class AlterNaturalPersonViewModel : AlterBaseEntityViewModel<NaturalPerson>, IAlterNaturalPersonViewModel {

        private readonly IEventAggregator _eventAggregator;
        private IUnityContainer _container;
        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }
        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Add};
        public override UIOperation UIOperation {
            get { return _operation; }
        }

        [InjectionConstructor] public AlterNaturalPersonViewModel(NaturalPerson entity, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _eventAggregator = eventAggregator;
        }

        public string BirthDate {
            get { return Entity.BirthDate.ToShortDateString(); }
            set {
                if(Entity.BirthDate.ToShortDateString() == value) return;

                DateTime parsed;
                if(DateTime.TryParse(value, out parsed)) Entity.BirthDate = parsed;
            }
        }

        public override void InitializeServices() {
            ClearEntity(null);
        }

        public override void Refresh() {
            ClearEntity(null);
        }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) {
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(UIOperation);
        }

        protected override void QuickSearch(object arg) {
            _eventAggregator.GetEvent<QuickSearchEvent>().Publish(UIOperation);
        }

        protected override void ClearEntity(object arg) {
            Entity = new NaturalPerson {};
        }

    }
}