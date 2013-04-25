#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Domain.SubEntity;
using LOB.UI.Contract.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    [Export(typeof(IAlterAddressViewModel)), Export(typeof(AlterBaseEntityViewModel<Address>))]
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

        public IList<string> Statuses {
            get {
                if(_statuses != null) return _statuses;
                _statuses = new List<string>(AddressStatusDictionary.Statuses.Keys);
                Status = _statuses.FirstOrDefault();
                return _statuses;
            }
        }

        public override void InitializeServices() {
            base.InitializeServices();
            Worker.DoWork += UpdateUFList;
            Worker.RunWorkerAsync();
        }

        private void UpdateUFList(object sender, DoWorkEventArgs doWorkEventArgs) { UFs = new ObservableCollection<UF>(Enum.GetValues(typeof(UF)).Cast<UF>()); }

        protected override void ClearEntityExecute(object arg) {
            base.ClearEntityExecute(arg);
            UF = Entity.State.ToUF();
        }
    }
}