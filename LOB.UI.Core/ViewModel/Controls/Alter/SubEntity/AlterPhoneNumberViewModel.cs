#region Usings

using System;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity
{
    public sealed class AlterPhoneNumberViewModel : AlterBaseEntityViewModel<PhoneNumber>, IAlterPhoneNumberViewModel
    {
        public AlterPhoneNumberViewModel(PhoneNumber entity, IRepository repository) : base(entity, repository)
        {
        }

        protected override void QuickSearch(object arg)
        {
            throw new NotImplementedException();
        }

        protected override void ClearEntity(object arg)
        {
            throw new NotImplementedException();
        }

        public override Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.AlterPhoneNumber; }
        }
    }
}