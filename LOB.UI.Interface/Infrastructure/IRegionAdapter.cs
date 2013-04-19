﻿using System.ComponentModel.Composition;

namespace LOB.UI.Interface.Infrastructure {
    [InheritedExport]
    public interface IRegionAdapter {
        void AddView<TView>(TView view, string regionName) where TView : IBaseView;
        void AddView<TView,TViewModel>(TView view,TViewModel viewModel, string regionName) where TViewModel : IBaseViewModel;
        IBaseView<TViewModel> GetView<TViewModel>(ViewID param, string regionName) where TViewModel :IBaseViewModel;
        IBaseView GetView(ViewID param, string regionName);
        /// <summary>
        ///     Remove a view from a region
        /// </summary>
        /// <param name="param">Unique Operation for that View</param>
        /// <param name="regionName">Name of the region found in RegionName class</param>
        void RemoveView(ViewID param, string regionName);
        bool ContainsView(ViewID param, string regionName);
    }
}