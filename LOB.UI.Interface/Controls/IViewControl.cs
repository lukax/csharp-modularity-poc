namespace LOB.UI.Interface.Controls {
    public interface IViewControl<out TViewModel> : IBaseView<TViewModel> where TViewModel : IBaseViewModel {}
}