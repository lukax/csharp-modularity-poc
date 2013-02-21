#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterClientViewModel : AlterPersonViewModel
    {
        #region Props

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
                _client.Status = value;
                OnPropertyChanged();
            }
        }

        public IList<Sale> BoughtHistory
        {
            get { return _client.BoughtHistory; }
            set
            {
                if (_client.BoughtHistory == value) return;
                _client.BoughtHistory = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [ImportingConstructor]
        public AlterClientViewModel(Client entity, IRepository repository)
            : base(entity.Person, repository)
        {
            _client = entity;
        }

        private Client _client { get; set; }
    }
}