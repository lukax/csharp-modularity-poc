#region Usings

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight.Threading;
using LOB.Dao.Interface;
using LOB.UI.Core.View;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;
using Microsoft.Win32;

#endregion

namespace LOB.UI.Core
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [Export] private IUnityContainer _container = new UnityContainer();
        [Import] private INavigator _navigator;
        [Import] private IRegionAdapter _regionAdapter;
        [Import] private ISessionCreator _sessionCreator;

        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            OnStartup();

            //OnStartup MainWindow
            _navigator.Startup<MainWindow>();
        }

        protected void OnStartup()
        {
            //USING LOB.DAO.NHIBERNATE IN SESSIONCREATOR IMPLEMENTATION
            ComposablePartCatalog daoDll;
            try
            {
                daoDll = new AssemblyCatalog("LOB.DAO.Nhibernate.dll");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No DAO was found please refer to dll");
                var dlg = new OpenFileDialog();
                dlg.FileName = "LOB.DAO.Nhibernate.dll";
                dlg.Filter = "Class Library (.dll)|*.dll"; // Filter files by extension 
                bool? check = dlg.ShowDialog();
                if (check.Value == false)
                    Thread.CurrentThread.Abort();

                string filename = dlg.FileName;
                daoDll = new DirectoryCatalog(filename);
            }

            var container =
                new CompositionContainer(new AggregateCatalog(
                                             new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                                             new AssemblyCatalog(Assembly.Load("LOB.Dao.Nhibernate"))
                                             ));
            container.ComposeParts(this);
        }
    }
}