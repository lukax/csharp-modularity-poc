#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public class AlterPersonViewModel : AlterBaseEntityViewModel<LocalPerson>, IAlterPersonViewModel {

        private readonly IPersonFacade _personFacade;
        private AlterAddressViewModel _alterAddressViewModel;
        private AlterContactInfoViewModel _alterContactInfoViewModel;

        [InjectionConstructor]
        public AlterPersonViewModel(IPersonFacade personFacade, AlterAddressViewModel alterAddressViewModel,
            AlterContactInfoViewModel alterContactInfoViewModel, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger,
            LocalPerson entity = null)
            : base(entity, repository, eventAggregator, logger) {
            _personFacade = personFacade;
            _alterAddressViewModel = alterAddressViewModel;
            _alterContactInfoViewModel = alterContactInfoViewModel;

            ////TODO: Use business logic to set default params
            //if(entity != null)
            //    if(Entity.Address.State == null && Entity.Address.Country == null) {
            //        Entity.Address.Country = "Brasil";
            //        Entity.Address.State = UFDictionary.Ufs[UF.RJ];
            //    }
        }

        public IAlterAddressViewModel AlterAddressViewModel { get { return _alterAddressViewModel; } set { _alterAddressViewModel = value as AlterAddressViewModel; } }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get { return _alterContactInfoViewModel; } set { _alterContactInfoViewModel = value as AlterContactInfoViewModel; } }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _personFacade.CanAdd(out results);
        }
        protected override void ClearEntity(object arg) { throw new NotImplementedException(); }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Add};
        #region Overrides of BaseViewModel

        public override void InitializeServices() { }
        public override void Refresh() { }

        #endregion
    }

    public class LocalPerson : Person {

    }
}