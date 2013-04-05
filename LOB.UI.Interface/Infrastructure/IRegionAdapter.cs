namespace LOB.UI.Interface.Infrastructure {
    public interface IRegionAdapter {

        void AddView<TView>(TView view, string regionName) where TView : IBaseView;
        IBaseView GetView(UIOperation param, string regionName);
        /// <summary>
        /// Remove a view from a region
        /// </summary>
        /// <param name="param">Unique Operation for that View</param>
        /// <param name="regionName">Name of the region found in RegionName class</param>
        void RemoveView(UIOperation param, string regionName);
        bool ContainsView(UIOperation param, string regionName);

    }
}