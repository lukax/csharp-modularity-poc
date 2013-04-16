#region Usings

using LOB.UI.Interface.ViewModel.Controls.Alter.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter {
    public interface IAlterEmployeeViewModel : IAlterBaseEntityViewModel {
        IAlterNaturalPersonViewModel AlterNaturalPersonViewModel { get; set; }
    }
}