#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterLegalPersonViewModel))]
    public sealed class AlterLegalPersonViewModel : AlterBaseEntityViewModel<LegalPerson>, IAlterLegalPersonViewModel {
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.LegalPerson, State = ViewState.Add};
        private AlterPersonViewModel _alterPersonViewModel;
        public IAlterPersonViewModel AlterPersonViewModel {
            get { return _alterPersonViewModel; }
            set { _alterPersonViewModel = value as AlterPersonViewModel; }
        }

        [ImportingConstructor]
        public AlterLegalPersonViewModel(ILegalPersonFacade legalPersonFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger, IAlterPersonViewModel alterPersonViewModel)
            : base(legalPersonFacade, repository, eventAggregator, logger) { AlterPersonViewModel = alterPersonViewModel; }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            AlterPersonViewModel.InitializeServices();
        }

        protected override bool CanSaveChanges(object arg) {
            if(ReferenceEquals(Entity, null)) return false;
            if(ViewID.State == ViewState.Add) return base.CanSaveChanges(arg) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            if(ViewID.State == ViewState.Update) return base.CanSaveChanges(arg) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            return false;
        }

        protected override void EntityChanged() {
            base.EntityChanged();
            _alterPersonViewModel.Entity = Entity;
        }

        public override void Dispose() {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }
    }
}