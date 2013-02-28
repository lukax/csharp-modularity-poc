#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public sealed class ListClientViewModel : ListBaseEntityViewModel<Client>
    {
        #region Props

        #endregion

        public ListClientViewModel(Client client, IRepository repository)
            : base(client, repository)
        {
        }

        public override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}