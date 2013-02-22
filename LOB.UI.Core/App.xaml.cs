#region Usings

using System;
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
using MefContrib.Integration.Unity;
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
        private ComposablePartCatalog _catalog;
        private INavigator _navigator;
        private IRegionAdapter _regionAdapter;
        private ISessionCreator _sessionCreator;
        private IUnityContainer _unityContainer;

        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            OnStartup();

            //Startup MainWindow
            _navigator.Startup<MainWindow>();
        }

        protected void OnStartup()
        {
            _catalog = LoadDlls();
            _unityContainer = new UnityContainer();

            //Make container resolve types known to MEF:
            //_unityContainer.AddNewExtension<CompositionIntegration>();
            //_unityContainer.Configure<CompositionIntegration>().Catalogs.Add(_catalog);

            //Full integration with MEF:
            _unityContainer.RegisterCatalog(_catalog);

            _navigator = _unityContainer.Resolve<INavigator>();
            _regionAdapter = _unityContainer.Resolve<IRegionAdapter>();
            _sessionCreator = _unityContainer.Resolve<ISessionCreator>();
        }

        private AggregateCatalog LoadDlls()
        {
            //USING LOB.DAO.NHIBERNATE IN SESSIONCREATOR IMPLEMENTATION

            ComposablePartCatalog daoDll = null;
            ComposablePartCatalog currentDll = null;
            ComposablePartCatalog domainDll = null;
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

                //Stop thread if no DAO was selected
                if (check.Value == false)
                    Thread.CurrentThread.Abort();

                string filename = dlg.FileName;
                daoDll = new DirectoryCatalog(filename);
            }
            finally
            {
                currentDll = new AssemblyCatalog(Assembly.GetExecutingAssembly());
                domainDll = new AssemblyCatalog(Assembly.Load("LOB.Domain"));
            }

            return new AggregateCatalog(daoDll, currentDll);
        }

    }
}