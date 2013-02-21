using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Base;

namespace LOB.UI.Core.ViewModel.Controls
{
    public class AlterAddressViewModel : AlterEntityViewModel<Address>
    {
        public AlterAddressViewModel(Address Entity)
            :base(Entity)
        {

        }
    }
}