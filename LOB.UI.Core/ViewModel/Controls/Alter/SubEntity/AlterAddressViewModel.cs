#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterAddressViewModel))]
    public sealed class AlterAddressViewModel : AlterBaseEntityViewModel<Address>, IAlterAddressViewModel {
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
        public string Status {
            get { return _status; }
            set {
                _status = value;
                Entity.Status = AddressStatusDictionary.Statuses[value];
            }
        }

        [ImportingConstructor]
        public AlterAddressViewModel(IRepository repository, IAddressFacade addressFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(addressFacade, repository, eventAggregator, logger) { }

        public IList<string> Statuses {
            get {
                if(_statuses != null) return _statuses;
                _statuses = new List<string>(AddressStatusDictionary.Statuses.Keys);
                Status = _statuses.FirstOrDefault();
                return _statuses;
            }
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            Worker.DoWork += UpdateUFList;
            Worker.RunWorkerAsync();
            EventAggregator.GetEvent<IncludeEntityEvent>().Subscribe(Include);
        }

        private void UpdateUFList(object sender, DoWorkEventArgs doWorkEventArgs) { UFs = new ObservableCollection<UF>(Enum.GetValues(typeof(UF)).Cast<UF>()); }

        protected override void ClearEntity(object arg) {
            base.ClearEntity(arg);
            UF = Entity.State.ToUF();
        }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Address, State = ViewState.Add};
    }
}