#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListClientViewModel : ListPersonViewModel
    {
        #region Props

        private Lazy<IQueryable<Client>> _clients;
        public IList<Client> Clients { get { return _clients.Value.ToList(); } }

        private Client _client;
        public Client Client
        {
            get { return _client; }
            set
            {
                if (_client == value) return; _client = value; OnPropertyChanged();
            }
        }

        #endregion
        public ListClientViewModel(Client client, Person entity, IRepository repository)
            : base(entity, repository)
        {
            Client = client;
            Client.Person = entity;

            _clients = new Lazy<IQueryable<Client>>(Repository.GetList<Client>);
        }
    }
}