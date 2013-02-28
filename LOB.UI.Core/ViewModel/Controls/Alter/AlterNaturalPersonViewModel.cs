#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter
{
    public class AlterNaturalPersonViewModel : AlterPersonViewModel<NaturalPerson>
    {

        public AlterNaturalPersonViewModel(NaturalPerson entity, IRepository repository,
                                           AlterAddressViewModel alterAddressViewModel,
                                           AlterContactInfoViewModel alterContactInfoViewModel)
            : base(entity, repository, alterAddressViewModel, alterContactInfoViewModel)
        {
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