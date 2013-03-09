#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    [Export]
    public partial class MainWindow : MetroWindow, IView
    {
        private ICommandService _commandService = CommandService.Default;
        private IUnityContainer _container;
        private IFluentNavigator _navigator;
        private MainWindowViewModel _viewModel;

        [ImportingConstructor]
        public MainWindow(IUnityContainer container, MainWindowViewModel viewModel, IFluentNavigator navigator)
        {
            _container = container;
            ViewModel = viewModel;
            _navigator = navigator;

            InitializeComponent();
        }

        public MainWindowViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                DataContext = value;
                _commandService.RegisterCommand("OpenTab", new DelegateCommand(OpenTab));
                _commandService.RegisterCommand("Cancel", new DelegateCommand(o => TabControlMain.Items.RemoveAt(((int?) o) ?? 0)));
                _commandService.RegisterCommand("OpenView", new DelegateCommand(o => OpenView(o, o)));
                _commandService.RegisterCommand("QuickSearch", new DelegateCommand(o => OpenView("QuickSearch", o)));
                Messenger.Default.Register<int?>(DataContext, "Cancel", o => TabControlMain.Items.RemoveAt(o ?? 0));
                Messenger.Default.Register<object>(DataContext, "OpenTab", OpenTab);
                Messenger.Default.Register<object>(DataContext, "OpenView", o => OpenView(o.ToString(), _navigator.ResolveView(o.ToString()).Get()));
                Messenger.Default.Register<object>(DataContext, "QuickSearch", o => OpenView("QuickSearch", o));
            }
        }

        public string Header { get; set; }
        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
            base.DataContext = _viewModel;
            base.UpdateLayout();
            ChangeFlyouts(null, null);
        }

        private void OpenView(object arg, object viewModel, bool asDialog = true)
        {
            if (viewModel is string)
                _navigator.ResolveView(arg.ToString(), _navigator.ResolveView(viewModel.ToString()).Get())
                          .Show(asDialog);
            else
                _navigator.ResolveView(arg.ToString(), viewModel);

            ChangeFlyouts(null, null);
        }

        public void OpenTab(object view)
        {
            if (view == null) throw new ArgumentNullException();
            if (!(view is IView)) throw new ArgumentException("Content isn't a IView");

            var t = new TabItem {Content = view, Header = ((IView) view).Header};

            ((IView) t.Content).Index = TabControlMain.Items.Add(t);
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

        private void ButtonLicense_OnClick(object sender, RoutedEventArgs e)
        {
            ContextMenuLicense.PlacementTarget = this;
            ContextMenuLicense.IsOpen = !ContextMenuLicense.IsOpen;
        }

        private void ButtonThemes_OnClick(object sender, RoutedEventArgs e)
        {
            ContextMenuThemes.PlacementTarget = this;
            ContextMenuThemes.IsOpen = !ContextMenuThemes.IsOpen;
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