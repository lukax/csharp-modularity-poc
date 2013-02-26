#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterClientViewModel : AlterBaseEntityViewModel<Client>
    {
        #region Props

        public IList<Store> ClientOf {
            get { return Entity.ClientOf; }
            set {
                if (Entity.ClientOf == value) return;
                Entity.ClientOf = value;
                OnPropertyChanged();
            }
        }

        public ClientStatus ClientStatus {
            get { return Entity.Status; }
            set {
                if (Entity.Status == value) return;
                Entity.Status = value;
                OnPropertyChanged();
            }
        }

        public IList<Sale> BoughtHistory {
            get { return Entity.BoughtHistory; }
            set {
                if (Entity.BoughtHistory == value) return;
                Entity.BoughtHistory = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [ImportingConstructor]
        public AlterClientViewModel(Client client, IRepository repository,
                                    AlterPersonViewModel alterPersonViewModel,
                                    AlterAddressViewModel alterAddressViewModel,
                                    AlterContactInfoViewModel alterContactInfoViewModel)
            : base(client, repository) {
            Entity = client;
        }

        private new Client Entity { get; set; }

        public override bool CanSaveChanges(object arg) {
            return true;
        }

        public override bool CanCancel(object arg) {
            return true;
        }

        public override void SaveChanges(object arg) {
            using (Repository.Uow) {
                Repository.Uow.BeginTransaction();
                Repository.Uow.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        public override void InitializeServices() {
        }

        public override void Refresh() {
        }
    }
}