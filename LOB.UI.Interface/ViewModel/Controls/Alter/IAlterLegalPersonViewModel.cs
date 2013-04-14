#region Usings

using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter {
    public interface IAlterLegalPersonViewModel : IAlterBaseEntityViewModel {
        IAlterPersonViewModel AlterPersonViewModel { get; set; }
    }
}