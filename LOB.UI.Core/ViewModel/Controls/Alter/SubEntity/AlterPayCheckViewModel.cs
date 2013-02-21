using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Base;

namespace LOB.UI.Core.ViewModel.Controls
{
    public class AlterPayCheckViewModel : AlterEntityViewModel<PayCheck>
    {
        public AlterPayCheckViewModel(PayCheck Entity)
            :base(Entity)
        {

        }
    }
}