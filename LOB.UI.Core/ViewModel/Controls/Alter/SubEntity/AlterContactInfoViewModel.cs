#region Usings

using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterContactInfoViewModel : AlterBaseEntityViewModel<ContactInfo>
    {
        public AlterContactInfoViewModel(ContactInfo entity, IRepository repository)
            : base(entity, repository)
        {
        }

        protected override void QuickSearch(object arg)
        {
            throw new System.NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new System.NotImplementedException();
        }

        protected override bool CanSaveChanges(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}