#region Usings

using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LOB.Log.Interface;
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.Resources.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    public partial class ShellWindow : MetroWindow, IBaseView
    {
        private static bool _loaded = false;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;
        private readonly IRegionManager _region;
        private IModuleManager _module;
        
        public ShellWindow(IUnityContainer container, IRegionManager region, ILogger logger,
                           IEventAggregator eventAggregator)
        {
            //CULTURE INFO
            Strings.Culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
            //
            _container = container;
            _region = region;
            _logger = logger;
            _eventAggregator = eventAggregator;
            InitializeComponent();
            OnLoad();
        }

        private void OnLoad()
        {
            _eventAggregator.GetEvent<CloseViewEvent>().Subscribe((o) => { if (o == OperationType.Main) this.Close(); });
            
            if (_loaded) return;
            _module = _container.Resolve<IModuleManager>();
            _module.LoadModule("UICoreViewModule");
            _logger.Log("Shell window First Initialized", Category.Debug, Priority.Low);
            _loaded = true;
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

        public Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.Main; }
        }

        private void TabRegion_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabRegion.SelectedIndex = TabRegion.Items.Count;
        }
    }
}