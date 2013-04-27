#region Usings

using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.UI.Contract;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel;
using MahApps.Metro;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;

#endregion

namespace LOB.UI.Core.View {
    [Export]
    public partial class Shell : IBaseView<ShellViewModel> {
        [Import] public IEventAggregator EventAggregator { get; set; }
        [Import] public ILoggerFacade Logger { get; set; }
        [Import] public IModuleManager ModuleManager { get; set; }
        public ShellViewModel ViewModel {
            get { return DataContext as ShellViewModel; }
            set { DataContext = value; }
        }

        public int Index { get; set; }

        public Shell() {
            Strings.Culture = new CultureInfo(ConfigurationManager.AppSettings["Culture"]); //INFO: CULTURE
            InitializeComponent();

            ContentRendered += OnLoad;
        }

        public void Refresh() { UpdateLayout(); }

        private void OnLoad(object sender, EventArgs eventArgs) {
            //LazyEventAggregator.GetEvent<CloseViewEvent>().Subscribe(o => { if(o.Equals(new ViewModelInfo {Type = ViewType.Main})) Close(); });
            //LazyEventAggregator.GetEvent<OpenViewEvent>().Subscribe(type => {
            //                                                        if(type.ViewState == ViewState.QuickSearchExecute) {
            //                                                            BlurModal.Radius = 15;
            //                                                            BorderModal.Visibility = Visibility.Visible;
            //                                                        }
            //                                                    });
            //LazyEventAggregator.GetEvent<CloseViewEvent>().Subscribe(type => {
            //                                                         if(type.ViewState == ViewState.QuickSearchExecute) {
            //                                                             BlurModal.Radius = 0;
            //                                                             BorderModal.Visibility = Visibility.Hidden;
            //                                                         }
            //                                                         if(type.Type == ViewType.Main) Close();
            //                                                     });
            EventAggregator.GetEvent<NotificationEvent>()
                           .Publish(new Notification {Message = Strings.App_License_Information, Type = NotificationType.Warning});

            ModuleManager.LoadModule("UICoreViewModule");
            //_logger.Log("Shell window First Initialized", Category.Debug, Priority.Low);

            var defaultView = CollectionViewSource.GetDefaultView(TabRegion.Items);
            defaultView.CollectionChanged += TabRegion_OnSelectionChanged;
        }

        private async void TabRegion_OnSelectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            TabRegion.SelectedIndex = -1;
            //ProgressRing.IsActive = true;
            await Task.Delay(150); //BUG: STUPID RESOURCES LOADING ASYNC
            // Fix validation color border in textboxes
            //ProgressRing.IsActive = false;
            //await Task.Delay(1);
            TabRegion.SelectedIndex = TabRegion.Items.Count - 1;
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
        #region Themes

// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
        private void MiLightGrey() { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Grey"), Theme.Light); }

        private void MiLightRed(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Light); }

        private void MiDarkRed(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Red"), Theme.Dark); }

        private void MiLightGreen(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Light); }

        private void MiDarkGreen(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Green"), Theme.Dark); }

        private void MiLightBlue(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Light); }

        private void MiDarkBlue(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Blue"), Theme.Dark); }

        private void MiLightPurple(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Light); }

        private void MiDarkPurple(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Purple"), Theme.Dark); }

        private void MiDarkOrange(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Orange"), Theme.Dark); }

        private void MiLightOrange(object sender, RoutedEventArgs e) { ThemeManager.ChangeTheme(this, ThemeManager.DefaultAccents.First(a => a.Name == "Orange"), Theme.Light); }
        // ReSharper restore UnusedMember.Local
        // ReSharper restore UnusedParameter.Local

        #endregion
    }
}