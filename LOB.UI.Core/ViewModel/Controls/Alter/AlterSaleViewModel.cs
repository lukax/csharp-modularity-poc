using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Sale>
    {
        public AlterSaleViewModel(Sale entity, IRepository repository) : base(entity, repository)
        {
        }
    }
}