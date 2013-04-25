namespace LOB.UI.Contract {
    /// <summary>
    ///     Internal usage only
    /// </summary>
    public interface IBaseView : IUIComponent {
        //int Index { get; set; }
        //void Refresh();
    }

    public interface IBaseView<out TViewModel> : IBaseView where TViewModel : IBaseViewModel {
        TViewModel ViewModel { get; }
    }
}