#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel;
using LOB.UI.Interface;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    [Export]
    public partial class MainWindow : MetroWindow, IView
    {
        private IUnityContainer _container;
        private INavigator _navigator;
        private MainWindowViewModel _viewModel;

        [ImportingConstructor]
        public MainWindow(IUnityContainer container, MainWindowViewModel viewModel, INavigator navigator)
        {
            _container = container;
            _viewModel = viewModel;
            _navigator = navigator;

            InitializeComponent();
            MiLightGrey();
        }

        public void InitializeServices()
        {
            DataContext = _viewModel;

            Messenger.Default.Register<int?>(DataContext, "Cancel", o => TabControlMain.Items.RemoveAt(o ?? 0));
            Messenger.Default.Register<object>(DataContext, "OpenTab", OpenTab);
            Messenger.Default.Register<object>(DataContext, "QuickSearch", vM => OpenView("QuickSearch", vM));
        }

        public void Refresh()
        {
            base.DataContext = _viewModel;
            base.UpdateLayout();
            ChangeFlyouts(null, null);
        }

        private void OpenView(object arg, object viewModel, bool asDialog = true)
        {
            _navigator.ResolveView(arg.ToString(), viewModel).StartView(asDialog);
            ChangeFlyouts(null, null);
        }

        public void OpenTab(object view)
        {
            if (view == null) throw new ArgumentNullException();
            if (!(view is ITabProp)) throw new ArgumentException("Content isn't a ITabProp");

            var t = new TabItem {Content = view, Header = ((ITabProp) view).Header};

            ((ITabProp) t.Content).Index = TabControlMain.Items.Add(t);
            TabControlMain.SelectedItem = t;
            ChangeFlyouts(null, null);
        }


        public void ChangeFlyouts(object sender, EventArgs eventArgs, bool isOpen = false)
        {
            foreach (var flyout in Flyouts)
            {
                flyout.IsOpen = isOpen;
            }
        }

        public void OpenOperationFlyout(object sender, EventArgs eventArgs)
        {
            Flyouts[0].IsOpen = !Flyouts[0].IsOpen;
        }

        public void OpenSellFlyout(object sender, EventArgs eventArgs)
        {
            Flyouts[1].IsOpen = !Flyouts[1].IsOpen;
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