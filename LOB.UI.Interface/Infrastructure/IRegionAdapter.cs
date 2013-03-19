namespace LOB.UI.Interface.Infrastructure
{
    public interface IRegionAdapter
    {
        void AddView<TView>(TView view, string regionName) where TView : IBaseView;
        IBaseView GetView(OperationType param, string regionName);
        void RemoveView(OperationType param, string regionName);
    }
}