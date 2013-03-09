#region Usings

using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter.Base
{
    public interface IAlterPersonViewModel<T> : IAlterBaseEntityViewModel<T> where T : Person
    {
        IAlterAddressViewModel AlterAddressViewModel { get; set; }
        IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }
    }
}