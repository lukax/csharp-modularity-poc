#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Sale>
    {
        public AlterSaleViewModel(Sale entity, IRepository repository) : base(entity, repository)
        {
        }

        protected override void QuickSearch(object arg)
        {
            throw new System.NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            Entity = new Sale();
        }
    }
}