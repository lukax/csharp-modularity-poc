using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>  
    {
        public AlterContactInfoViewModel(ContactInfo Entity)
            :base(Entity)
        {

        }
    }
}