namespace LOB.UI.Interface.Infrastructure
{
    public interface IRegionAdapter
    {
        void AddView<TView>(TView view, string regionName) where TView : IBaseView;
        IBaseView GetView(string param, string regionName);
        void RemoveView(string param, string regionName);
    }
}