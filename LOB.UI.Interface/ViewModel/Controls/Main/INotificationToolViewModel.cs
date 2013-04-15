#region Usings

using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Main {
    public interface INotificationToolViewModel : IBaseViewModel {
        ICommand DismissCommand { get; set; }
        bool IsVisible { get; set; }
        string Status { get; }
    }
}