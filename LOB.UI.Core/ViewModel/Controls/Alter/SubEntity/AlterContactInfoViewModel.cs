using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>  
    {
        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository)
            :base(entity, repository)
        {

        }
    }
}