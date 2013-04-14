#region Usings

using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter {
    public interface IAlterNaturalPersonViewModel : IAlterBaseEntityViewModel {
        IAlterPersonViewModel AlterPersonViewModel { get; set; }
    }
}