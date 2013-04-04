#region Usings

using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LOB.Core.Localization;
using LOB.Log.Interface;
using LOB.UI.Core.Events.View;
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

namespace LOB.UI.Core.View {
    public partial class Shell : MetroWindow, IBaseView {
        private static bool _loaded;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;
        private readonly IRegionManager _region;
        private IModuleManager _module;

        public Shell(IUnityContainer container, IRegionManager region, ILogger logger,
            IEventAggregator eventAggregator) {
            //CULTURE INFO
            Strings.Culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]);
            //
            _container = container;
            _region = region;
            _logger = logger;
            _eventAggregator = eventAggregator;
            InitializeComponent();
            OnLoad();

            //Change to Last added Tab
            var defaultView = CollectionViewSource.GetDefaultView(TabRegion.Items);
            defaultView.CollectionChanged += TabRegion_OnSelectionChanged;
        }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header { get; set; }
        public int Index { get; set; }

        public void InitializeServices() {
            _eventAggregator.GetEvent<OpenViewEvent>().Subscribe(type => {
                                                                     if(type.State ==
                                                                        UIOperationState.QuickSearch) {
                                                                         BlurModal.Radius = 10;
                                                                         BorderModal.Visibility =
                                                                             Visibility.Visible;
                                                                     }
                                                                 });
            _eventAggregator.GetEvent<CloseViewEvent>().Subscribe(type => {
                                                                      if(type.State ==
                                                                         UIOperationState
                                                                             .QuickSearch) {
                                                                          BlurModal.Radius = 0;
                                                                          BorderModal.Visibility =
                                                                              Visibility.Hidden;
                                                                      }
                                                                  });
        }

        public void Refresh() {
            base.UpdateLayout();
            MiLightBlue(null, null);
        }

        public UIOperationType UIOperationType {
            get { return UIOperationType.Main; }
        }

        private void OnLoad() {
            _eventAggregator.GetEvent<CloseViewEvent>()
                            .Subscribe(
                                o => { if(o == new UIOperation {Type = UIOperationType.Main}) Close(); });

            if(_loaded) return;
            _module = _container.Resolve<IModuleManager>();
            _module.LoadModule("UICoreViewModule");
            _logger.Log("Shell window First Initialized", Category.Debug, Priority.Low);
            _loaded = true;
        }

        private async void TabRegion_OnSelectionChanged(object sender,
            NotifyCollectionChangedEventArgs e) {
            TabRegion.SelectedIndex = -1;
            ProgressRing.IsActive = true;
            await Task.Delay(300);
                // Fix validation color border in textboxes TODO: Check this issue
            ProgressRing.IsActive = false;
            TabRegion.SelectedIndex = TabRegion.Items.Count - 1;
        }

        public UIOperation Operation {
            get { return ViewModel.Operation; }
        }
        #region Themes

        private void MiLightGrey() {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Grey"),
                                     Theme.Light);
        }

        private void MiLightRed(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"),
                                     Theme.Light);
        }

        private void MiDarkRed(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"),
                                     Theme.Dark);
        }

        private void MiLightGreen(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"),
                                     Theme.Light);
        }

        private void MiDarkGreen(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"),
                                     Theme.Dark);
        }

        private void MiLightBlue(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"),
                                     Theme.Light);
        }

        private void MiDarkBlue(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"),
                                     Theme.Dark);
        }

        private void MiLightPurple(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this,
                                     ThemeManager.DefaultAccents.First(a => a.Name == "Purple"),
                                     Theme.Light);
        }

        private void MiDarkPurple(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this,
                                     ThemeManager.DefaultAccents.First(a => a.Name == "Purple"),
                                     Theme.Dark);
        }

        private void MiDarkOrange(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this,
                                     ThemeManager.DefaultAccents.First(a => a.Name == "Orange"),
                                     Theme.Dark);
        }

        private void MiLightOrange(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this,
                                     ThemeManager.DefaultAccents.First(a => a.Name == "Orange"),
                                     Theme.Light);
        }

        #endregion
    }
}