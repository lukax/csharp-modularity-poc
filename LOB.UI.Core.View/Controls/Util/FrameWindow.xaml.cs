#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using LOB.UI.Contract;
using LOB.UI.Core.Event.View;
using MahApps.Metro;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.View.Controls.Util {
    [Export]
    public partial class FrameWindow : IBaseView<IBaseViewModel> {
        [Import] public IEventAggregator EventAggregator {
            set { value.GetEvent<CloseViewEvent>().Subscribe(o => Close()); }
        }

        public FrameWindow() { InitializeComponent(); }

        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public int Index { get; set; }

        public void Refresh() {
            UpdateLayout();
            MiLightBlue(null, null);
        }
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

        #endregion
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}