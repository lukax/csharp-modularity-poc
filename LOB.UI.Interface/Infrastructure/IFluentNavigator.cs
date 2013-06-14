#region Usings

using System;

#endregion

namespace LOB.UI.Contract.Infrastructure {
    public interface IFluentNavigator {
        IFluentNavigator Init { get; }
        //IBaseView<IBaseViewModel> Get();
        //IFluentNavigator SetView(Func<IBaseView<IBaseViewModel>> view);
        IFluentNavigator ResolveView(IViewInfo param);
        IFluentNavigator ResolveView(Type param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseViewModel;
        IFluentNavigator AddToRegion(string regionName);
        Guid GetViewId { get; }

    }

    //public sealed class OnOpenViewEventArgs : EventArgs {
    //    public OnOpenViewEventArgs(IBaseView<IBaseViewModel> baseView) { BaseView = baseView; }

    //    public IBaseView<IBaseViewModel> BaseView { get; private set; }
    //}

    //public delegate void OnOpenViewEventHandler(object sender, OnOpenViewEventArgs e);
}