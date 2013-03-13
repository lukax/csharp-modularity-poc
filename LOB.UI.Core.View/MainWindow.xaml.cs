#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    [Export]
    public partial class MainWindow : MetroWindow, IBaseView
    {
        private ICommandService _commandService = CommandService.Default;
        private IUnityContainer _container;
        private IFluentNavigator _navigator;
        private MainWindowViewModel _viewModel;

        //[ImportingConstructor]
        //public MainWindow(IUnityContainer container, MainWindowViewModel viewModel, IFluentNavigator navigator)
        //{
        //    _container = container;
        //    ViewModel = viewModel;
        //    _navigator = navigator;

        //    navigator.OnOpenView += (sender, args) => ChangeFlyouts(sender, args);

        //    container.Resolve<IAlterProductViewModel>();
        //    InitializeComponent();
        //}

        public IBaseViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                var mainWindowViewModel = value as MainWindowViewModel;
                if (mainWindowViewModel != null)
                    _viewModel = mainWindowViewModel;

                DataContext = value;
                _commandService.RegisterCommand("OpenTab", new DelegateCommand(OpenTab));
                _commandService.RegisterCommand("Cancel",
                                                new DelegateCommand(o => TabControlMain.Items.RemoveAt(((int?) o) ?? 0)));
                _commandService.RegisterCommand("OpenView", new DelegateCommand(o => OpenView(o.ToString())));
                _commandService.RegisterCommand("QuickSearch", new DelegateCommand(o => OpenView(o.ToString())));
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

        private void OpenView(string arg, bool asDialog = true)
        {
            _navigator.ResolveView(arg).ResolveViewModel(arg).Show(asDialog);
            ChangeFlyouts(null, null);
        }

        public void OpenTab(object view)
        {
            var baseView = view as IBaseView;
            if (baseView == null) throw new ArgumentException("Content isn't a IBaseView");
            var t = new TabItem {Content = view, Header = baseView.Header};
            ((IBaseView)t.Content).Index = TabControlMain.Items.Add(t);
            TabControlMain.SelectedItem = t;
            ChangeFlyouts(null, null);
        }


        public void ChangeFlyouts(object sender, EventArgs eventArgs, bool closeFlyout = true)
        {
            foreach (var flyout in Flyouts)
            {
                flyout.IsOpen = !closeFlyout;
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