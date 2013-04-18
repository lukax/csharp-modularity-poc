#region Usings

using System.ComponentModel.Composition;
using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.Main {
    [InheritedExport]
    public interface INotificationToolViewModel : IBaseViewModel {
        ICommand DismissCommand { get; set; }
        bool IsVisible { get; set; }
        string Status { get; }
    }
}