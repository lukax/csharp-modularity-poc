#region Usings

using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter.Base {
    public interface IAlterPersonViewModel : IAlterBaseEntityViewModel {

        IAlterAddressViewModel AlterAddressViewModel { get; }
        IAlterContactInfoViewModel AlterContactInfoViewModel { get; }

    }
}