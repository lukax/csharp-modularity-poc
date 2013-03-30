#region Usings
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LOB.Log.Interface;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Util {
    public partial class FrameWindow : MetroWindow, IBaseView {

        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger _logger;

        public FrameWindow(IUnityContainer container, ILogger logger, IEventAggregator eventAggregator) {
            this._container = container;
            this._logger = logger;
            this._eventAggregator = eventAggregator;
            this._eventAggregator.GetEvent<CloseViewEvent>().Subscribe(o => { this.Close(); });
            this.InitializeComponent();
        }

        public UserControl View { get; set; }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IBaseViewModel; }
            set { this.DataContext = value; }
        }

        public string Header { get; set; }
        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {
            base.UpdateLayout();
            this.MiLightBlue(null, null);
        }

        public OperationType OperationType {
            get { return OperationType.Main; }
        }
        #region Themes
        private void MiLightGrey() {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Grey"), Theme.Light);
        }

        private void MiLightRed(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Light);
        }

        private void MiDarkRed(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Dark);
        }

        private void MiLightGreen(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Light);
        }

        private void MiDarkGreen(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Dark);
        }

        private void MiLightBlue(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Light);
        }

        private void MiDarkBlue(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark);
        }

        private void MiLightPurple(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Light);
        }

        private void MiDarkPurple(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Dark);
        }

        private void MiDarkOrange(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Orange"), Theme.Dark);
        }

        private void MiLightOrange(object sender, RoutedEventArgs e) {
            ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Orange"), Theme.Light);
        }
        #endregion
        private void Busy() {
            this.ModalRegion = new BusyView();
        }

    }
}