using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Base;

namespace LOB.UI.Core.ViewModel.Controls
{
    public class AlterContactInfoViewModel : AlterEntityViewModel<ContactInfo>  
    {
        public AlterContactInfoViewModel(ContactInfo Entity)
            :base(Entity)
        {

        }
    }
}