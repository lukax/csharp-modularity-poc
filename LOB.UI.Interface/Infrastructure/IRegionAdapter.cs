namespace LOB.UI.Interface.Infrastructure {
    public interface IRegionAdapter {

        void AddView<TView>(TView view, string regionName) where TView : IBaseView;
        IBaseView GetView(UIOperation param, string regionName);
        void RemoveView(UIOperation param, string regionName);

    }
}