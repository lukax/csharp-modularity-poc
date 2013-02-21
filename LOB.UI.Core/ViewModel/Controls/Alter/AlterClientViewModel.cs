using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterClientViewModel : AlterPersonViewModel
    {
        public IList<Store> ClientOf
        {
            get { return _client.ClientOf; }
            set
            {
                if (_client.ClientOf == value) return;
                _client.ClientOf = value;
                OnPropertyChanged();
            }
        }
        public ClientStatus ClientStatus
        {
            get { return _client.Status; }
            set
            {
                if (_client.Status == value) return;
                _client.Status = value; OnPropertyChanged();
            }
        }
        public IList<Sale> BoughtHistory
        {
            get
            {
                return _client.BoughtHistory;
            }
            set
            {
                if (_client.BoughtHistory == value) return;
                _client.BoughtHistory = value;
                OnPropertyChanged();
            }
        }

        private Client _client { get; set; }

        [ImportingConstructor]
        public AlterClientViewModel(Client entity)
            : base(entity.Person)
        {
            _client = entity;
        }
    }
}