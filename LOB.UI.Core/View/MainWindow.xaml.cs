#region Usings

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Core;
using LOB.UI.Core.ViewModel;
using LOB.UI.Interface;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, IView, IComposableComponent
    {
        public MainWindow()
        {
            ComponentCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            ComponentContainer = new CompositionContainer(ComponentCatalog);
            _container = new UnityContainer();
            InitializeComponent();
            MiLightGrey();
        }

        [Import]
        private MainWindowViewModel ViewModel { get; set; }

        [Export]
        private IUnityContainer _container { get; set; }

        public CompositionContainer ComponentContainer { get; private set; }
        public ComposablePartCatalog ComponentCatalog { get; private set; }

        public void Compose(params object[] objects)
        {
            if (objects != null) ComponentContainer.ComposeParts(objects);
            ComponentContainer.ComposeParts(this);
        }

        public void OpenOperationFlyout(object sender, EventArgs eventArgs)
        {
            Flyouts[0].IsOpen = !Flyouts[0].IsOpen;
        }

        public void OpenSellFlyout(object sender, EventArgs eventArgs)
        {
            Flyouts[1].IsOpen = !Flyouts[1].IsOpen;
        }

        public void Dispose()
        {
            ComponentContainer.Dispose();
        }

        public void InitializeServices()
        {
            Compose();
            DataContext = ViewModel;

            //Registrations
            Messenger.Default.Register<int?>(DataContext, "Cancel", o => TabControlMain.Items.RemoveAt(o ?? 0));
            Messenger.Default.Register<object>(DataContext, "OpenTab", OpenTab);
        }

        public void Refresh()
        {
            base.DataContext = ViewModel;
            base.UpdateLayout();
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

        public void OpenTab(object content)
        {
            if (content == null) throw new ArgumentNullException();
            if(!(content is ITabProp)) throw new ArgumentException("Content isn't a ITabProp");
            
            var t = new TabItem {Content = content, Header = ((ITabProp) content).Header};

            ((ITabProp) t.Content).Index = TabControlMain.Items.Add(t);
            TabControlMain.SelectedItem = t;
        }
    }
}