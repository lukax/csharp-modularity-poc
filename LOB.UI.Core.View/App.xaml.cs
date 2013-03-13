#region Usings

using System.Windows;
using GalaSoft.MvvmLight.Threading;

#endregion

namespace LOB.UI.Core.View
{
    public sealed partial class App : Application
    {
        //private ComposablePartCatalog _catalog;
        //private IFluentNavigator _navigator;
        //private IRegionAdapter _regionAdapter;
        //private ISessionCreator _sessionCreator;
        //private IUnityContainer _unityContainer;

        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //OnStartup();
            new Bootstrapper().Run();

            ////Startup MainWindow
            //    _navigator.ResolveView<MainWindow>().Show();
        }

        //private void OnStartup()
        //{
        //    _catalog = LoadDlls();
        //    _unityContainer = new UnityContainer();
        //    _unityContainer.RegisterInstance<IServiceLocator>(new UnityServiceLocatorAdapter(_unityContainer));
        //    Make container resolve types known to MEF:
        //    _unityContainer.RegisterCatalog(_catalog);

        //    _unityContainer.RegisterInstance(CommandService.Default);

        //    _navigator = _unityContainer.Resolve<IFluentNavigator>();
        //    _regionAdapter = _unityContainer.Resolve<IRegionAdapter>();
        //    _sessionCreator = _unityContainer.Resolve<ISessionCreator>();

        //    Console.WriteLine(Assembly.GetExecutingAssembly().FullName);
        //    Console.WriteLine(Assembly.Load("LOB.Business").FullName);
        //    Console.WriteLine(Assembly.Load("LOB.Dao.Nhibernate").FullName);
        //    Console.WriteLine(Assembly.Load("LOB.UI.Core").FullName);
        //}

        //private AggregateCatalog LoadDlls()
        //{
        //    //USING LOB.DAO.NHIBERNATE IN SESSIONCREATOR IMPLEMENTATION
        //    ComposablePartCatalog daoDll = null;
        //    ComposablePartCatalog currentDll = null;
        //    ComposablePartCatalog domainDll = null;
        //    ComposablePartCatalog uiInterfaceDll = null;
        //    try
        //    {
        //        daoDll = new AssemblyCatalog("LOB.DAO.Nhibernate.dll");
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        MessageBox.Show("No DAO was found please refer to dll");
        //        var dlg = new OpenFileDialog
        //            {
        //                FileName = "LOB.DAO.Nhibernate.dll",
        //                Filter = "Class Library (.dll)|*.dll"
        //            };
        //        bool? check = dlg.ShowDialog();

        //        //Stop thread if no DAO was selected
        //        if (check.Value == false)
        //            Thread.CurrentThread.Abort();

        //        string filename = dlg.FileName;
        //        daoDll = new DirectoryCatalog(filename);
        //    }
        //    finally
        //    {
        //        currentDll = new AssemblyCatalog(Assembly.GetExecutingAssembly());
        //        domainDll = new AssemblyCatalog(Assembly.Load("LOB.Domain"));
        //        uiInterfaceDll = new AssemblyCatalog(Assembly.Load("LOB.UI.Interface"));
        //    }

        //    return new AggregateCatalog(daoDll, domainDll, currentDll, uiInterfaceDll);
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //}

        //private void Dispose(bool b)
        //{
        //    if (!b) return;
        //    _unityContainer.Dispose();
        //    GC.SuppressFinalize(this);
        //}
    }
}