#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterClientViewModel : AlterPersonViewModel
    {
        #region Props

        protected Client Client { get; set; }
        
        public IList<Store> ClientOf
        {
            get { return Client.ClientOf; }
            set
            {
                if (Client.ClientOf == value) return;
                Client.ClientOf = value;
                OnPropertyChanged();
            }
        }

        public ClientStatus ClientStatus
        {
            get { return Client.Status; }
            set
            {
                if (Client.Status == value) return;
                Client.Status = value;
                OnPropertyChanged();
            }
        }

        public IList<Sale> BoughtHistory
        {
            get { return Client.BoughtHistory; }
            set
            {
                if (Client.BoughtHistory == value) return;
                Client.BoughtHistory = value;
                OnPropertyChanged();
            }
        }

        #endregion

        [ImportingConstructor]
        public AlterClientViewModel(Client client, IRepository repository)
            : base(client.Person, repository)
        {
            Client = client;
        }

    }
}