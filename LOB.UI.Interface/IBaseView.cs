namespace LOB.UI.Interface {
    public interface IBaseView : IUIComponent {
        int Index { get; set; }
        void Refresh();
    }

    public interface IBaseView<TViewModel> : IBaseView where TViewModel : IBaseViewModel {
        TViewModel ViewModel { get; set; }
    }
}