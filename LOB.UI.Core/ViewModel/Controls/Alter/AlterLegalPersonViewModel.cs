#region Usings

using System.ComponentModel.Composition;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public sealed class AlterLegalPersonViewModel : AlterPersonViewModel<LegalPerson>
    {
        [ImportingConstructor]
        public AlterLegalPersonViewModel(LegalPerson entity, IRepository repository,
                                         AlterAddressViewModel alterAddressViewModel,
                                         AlterContactInfoViewModel alterContactInfoViewModel)
            : base(entity, repository, alterAddressViewModel, alterContactInfoViewModel)
        {
        }

        protected override void SaveChanges(object arg)
        {
            using (Repository.Uow)
            {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void QuickSearch(object arg)
        {
            throw new System.NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new System.NotImplementedException();
        }
    }
}