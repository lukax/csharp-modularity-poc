#region Usings

using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    [Export]
    public class AlterNaturalPersonViewModel : AlterPersonViewModel<NaturalPerson>
    {

        [ImportingConstructor]
        public AlterNaturalPersonViewModel(NaturalPerson entity, Address address, ContactInfo contactInfo, IRepository repository,
                                           AlterAddressViewModel alterAddressViewModel,
                                           AlterContactInfoViewModel alterContactInfoViewModel)
            : base(entity, address, contactInfo, repository, alterAddressViewModel, alterContactInfoViewModel)
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
            Entity = new NaturalPerson();
        }
    }
}