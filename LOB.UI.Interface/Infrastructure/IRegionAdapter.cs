#region Usings

using System;

#endregion

namespace LOB.UI.Contract.Infrastructure {
    public interface IRegionAdapter {
        void Add<TView>(TView view, string regionName) where TView : IBaseView<IBaseViewModel>;
        IBaseView<IBaseViewModel> Get(Guid viewId, string regionName = null);
        /// <summary>
        ///     Remove a view from a region
        /// </summary>
        /// <param name="view">Unique Operation for that View</param>
        /// <param name="regionName">Name of the region found in RegionName class</param>
        void Remove(IBaseView<IBaseViewModel> view, string regionName = null);
        bool Contains(IBaseView<IBaseViewModel> view, string regionName);
    }
}