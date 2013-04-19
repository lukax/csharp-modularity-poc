#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface {
    [InheritedExport]
    public interface IBaseView : IUIComponent {
        int Index { get; set; }
        void Refresh();
    }

    [InheritedExport]
    public interface IBaseView<TViewModel> : IBaseView where TViewModel : IBaseViewModel {
        TViewModel ViewModel { get; set; }
    }
}