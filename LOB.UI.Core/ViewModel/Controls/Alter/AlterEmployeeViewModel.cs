#region Usings

using System;
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
    public sealed class AlterEmployeeViewModel : AlterNaturalPersonViewModel
    {
        [ImportingConstructor]
        public AlterEmployeeViewModel(NaturalPerson entity, Address address, ContactInfo contactInfo, IRepository repository, AlterAddressViewModel alterAddressViewModel, AlterContactInfoViewModel alterContactInfoViewModel) : base(entity, address, contactInfo, repository, alterAddressViewModel, alterContactInfoViewModel)
        {
            
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

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }
    }
}