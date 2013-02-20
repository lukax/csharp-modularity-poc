#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.View.Controls;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core
{
    public class Navigator : INavigator
    {
        private readonly IUnityContainer _container;
        private readonly IRegionAdapter _regionAdapter;

        [ImportingConstructor]
        public Navigator(IUnityContainer container, IRegionAdapter regionAdapter)
        {
            _container = container;
            _regionAdapter = regionAdapter;
        }

        public void Startup<TView>() where TView : class
        {
            var view = _container.Resolve<TView>() as Window;
            if (view == null) return;
            if (view is IView) ((IView) view).InitializeServices();
            view.Show();
        }

        public void Startup<TView>(object viewModel) where TView : class
        {
            var view = _container.Resolve<TView>() as Window;
            if (view == null) return;
            if (view is IView) ((IView) view).InitializeServices();
            view.DataContext = viewModel;
            view.Show();
        }

        public void OpenView<TView>(string regionName) where TView : class
        {
            var view = _container.Resolve<TView>() as UserControl;
            if (view == null) return;
            if (view is IView) ((IView) view).InitializeServices();
            _regionAdapter.AddView(view, regionName);
        }

        public void OpenView<TView>(string regionName, object viewModel) where TView : class
        {
            var view = _container.Resolve<TView>() as UserControl;
            if (view == null) return;
            if (view is IView) ((IView) view).InitializeServices();
            dynamic model = viewModel;
            _regionAdapter.AddView(view, regionName, model.Title);
        }

        public object ResolveView(string param)
        {
            switch (param)
            {
                case "AlterProduct":
                    return _container.Resolve<AlterProductView>();
                case "ListProduct":
                    return _container.Resolve<ListProductView>();
                case "AlterEmployee":
                    return _container.Resolve<AlterEmployeeView>();
                case "ListEmployee":
                    return _container.Resolve<ListEmployeeView>();
                case "AlterClient":
                    return _container.Resolve<AlterClientView>();
                case "ListClient":
                    return _container.Resolve<ListClientView>();
                case "AlterSale":
                    return _container.Resolve<AlterSaleView>();
            }
            throw new ArgumentException("Parameter not implemented yet, ", "param");
        }
    }
}