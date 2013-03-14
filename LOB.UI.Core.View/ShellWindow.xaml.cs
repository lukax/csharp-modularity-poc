#region Usings

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Names;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Names;
using LOB.UI.Interface.ViewModel.Base;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    public partial class ShellWindow : MetroWindow, IBaseView
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _region;
        private ICommandService _commandService;

        BackgroundWorker bg = new BackgroundWorker();
        private IFluentNavigator _navigator;

        public ShellWindow(IUnityContainer container, IRegionManager region)
        {
            _container = container;
            _region = region;
            InitializeComponent();
            Load();
        }

        private void Busy()
        {
            ModalRegion = new BusyView();
        }

        private void Load()
        {
            var module = _container.Resolve<IModuleManager>();
            module.LoadModule("UICoreViewModule");
            _commandService = _container.Resolve<ICommandService>();
            _navigator = _container.Resolve<IFluentNavigator>();
            _commandService.RegisterCommand("OpenView", new DelegateCommand(
                o =>
                {
                    OperationNames parsed; Enum.TryParse(o.ToString(), out parsed);
                    var c = OperationTypes.Views[parsed];
                    _region.RegisterViewWithRegion(RegionNames.ModalRegion,c);
                    //var localregion = _region.Regions[RegionNames.ModalRegion];
                    //localregion.Activate(_container.Resolve<ListOpView>());
                }));
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header { get; set; }
        public int? Index { get; set; }

        public void InitializeServices()
        {
            Messenger.Default.Register<object>(DataContext, "Cancel", o => Close());
        }

        public void Refresh()
        {
            base.UpdateLayout();
            MiLightBlue(null, null);
        }

        #region Themes

        private void MiLightGrey()
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Grey"), Theme.Light);
        }

        private void MiLightRed(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Light);
        }

        private void MiDarkRed(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Dark);
        }

        private void MiLightGreen(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Light);
        }

        private void MiDarkGreen(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Dark);
        }

        private void MiLightBlue(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Light);
        }

        private void MiDarkBlue(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark);
        }

        private void MiLightPurple(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Light);
        }

        private void MiDarkPurple(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Dark);
        }

        private void MiDarkOrange(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Orange"), Theme.Dark);
        }

        private void MiLightOrange(object sender, RoutedEventArgs e)
        {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Orange"), Theme.Light);
        }

        #endregion
    }
}