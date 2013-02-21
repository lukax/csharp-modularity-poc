using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>
    {
        public AlterPayCheckViewModel(PayCheck payCheck, IRepository repository)
            :base(payCheck, repository)
        {

        }
    }
}