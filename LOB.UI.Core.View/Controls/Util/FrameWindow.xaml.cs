#region Usings

using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LOB.Log.Interface;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Base;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Util
{
    public partial class FrameWindow : MetroWindow, IBaseView
    {
        private static bool _moduleFirstLoaded = false;
        private readonly IUnityContainer _container;
        private readonly ILogger _logger;
        private readonly IRegionManager _region;
        private ICommandService _commandService;

        private IFluentNavigator _navigator;
        private BackgroundWorker bg = new BackgroundWorker();

        public FrameWindow(IUnityContainer container, IRegionManager region, ILogger logger,
                           ICommandService commandService)
        {
            _container = container;
            _region = region;
            _logger = logger;
            _commandService = commandService;
            _commandService.Register(OperationNames.Cancel, new DelegateCommand(Close));
            InitializeComponent();
        }

        public UserControl View { get; set; }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header { get; set; }
        public int? Index { get; set; }

        public void InitializeServices()
        {
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

        private void Close(object arg)
        {
            this.Close();
        }

        private void Busy()
        {
            ModalRegion = new BusyView();
        }
    }
}