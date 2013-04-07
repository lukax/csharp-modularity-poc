using System.Windows.Input;

namespace LOB.UI.Interface.ViewModel.Controls.Main {
    public interface INotificationToolViewModel : IBaseViewModel
    {
        ICommand DismissCommand { get; set; }
        bool IsVisible { get; set; }
        string Status { get; }
    }
}