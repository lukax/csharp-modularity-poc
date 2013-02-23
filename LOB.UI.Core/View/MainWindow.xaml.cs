#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain.Base;
using LOB.UI.Core.View.Controls;
using LOB.UI.Core.ViewModel;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;
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
        private MainWindowViewModel _viewModel;

        [ImportingConstructor]
        public MainWindow(IUnityContainer container, MainWindowViewModel viewModel)
        {
            _container = container;
            _viewModel = viewModel;

            InitializeComponent();
            MiLightGrey();
        }

        public void InitializeServices()
        {
            DataContext = _viewModel;

            //Registrations
            //Messenger.Default.Register<int?>(DataContext, "Cancel", o=> TabControlMain.Items.Remove(TabControlMain.SelectedItem) );
            Messenger.Default.Register<int?>(DataContext, "Cancel", o => TabControlMain.Items.RemoveAt(o ?? 0));
            Messenger.Default.Register<object>(DataContext, "OpenTab", OpenTab);
            Messenger.Default.Register<object>(DataContext, "QuickSearchCommand", OpenQuickSearch);
        }


        private void OpenQuickSearch(object arg)
        {
            //var v = navigator.ResolveView("");
            var v = new FrameWindow();
            v.Title = "Quick Search...";
            v.Frame.Content = new ListBaseEntityView()
                {
                    //DataContext = _container.Resolve<AlterBaseEntityViewModel<BaseEntity>>()
                };
            v.ShowDialog();
        }

        public void Refresh()
        {
            base.DataContext = _viewModel;
            base.UpdateLayout();
        }

        public void OpenTab(object content)
        {
            if (content == null) throw new ArgumentNullException();
            if (!(content is ITabProp)) throw new ArgumentException("Content isn't a ITabProp");

            var t = new TabItem {Content = content, Header = ((ITabProp) content).Header};

            ((ITabProp) t.Content).Index = TabControlMain.Items.Add(t);
            TabControlMain.SelectedItem = t;
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