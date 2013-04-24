#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public interface IRegionAdapter {
        void Add<TView>(TView view, string regionName) where TView : IBaseView<IBaseViewModel>;
        IBaseView<IBaseViewModel> Get(Guid viewId);
        /// <summary>
        ///     Remove a view from a region
        /// </summary>
        /// <param name="view">Unique Operation for that View</param>
        ///// <param name="regionName">Name of the region found in RegionName class</param>
        void Remove(IBaseView<IBaseViewModel> view);
        bool Contains(IBaseView<IBaseViewModel> view, string regionName);
    }
}