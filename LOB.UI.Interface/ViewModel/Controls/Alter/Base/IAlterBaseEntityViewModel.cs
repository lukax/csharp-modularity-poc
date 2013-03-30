#region Usings
using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Alter.Base {
    public interface IAlterBaseEntityViewModel : IBaseViewModel {

        int Index { get; set; }
        ICommand QuickSearchCommand { get; set; }
        ICommand SaveChangesCommand { get; set; }
        ICommand DiscardChangesCommand { get; set; }
        ICommand ExitCommand { get; set; }

    }
}