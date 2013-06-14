#region Usings

using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using LOB.Log;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;

#endregion

namespace LOB.UI.Core.View.Modularity {
    public class Bootstrapper : MefBootstrapper {
        protected override ILoggerFacade CreateLogger() { return new Logger(); }

        //protected override IModuleCatalog CreateModuleCatalog() {
        //    //Reference
        //    //var catalog = new ModuleCatalog();
        //    //Type businessModule = typeof(LOB.Business.Module);
        //    //Type daoModule = typeof(LOB.Dao.Nhibernate.Module);
        //    //Type uiCoreModule = typeof(LOB.UI.Core.Module);
        //    //Type uiCoreViewModule = typeof(LOB.UI.Core.View.Module);
        //    //Type iLoggerModule = typeof (LOB.Log.Module);
        //    //catalog.AddModule(new ModuleInfo() { ModuleName = "LogModule", ModuleType = iLoggerModule.AssemblyQualifiedName });
        //    //catalog.AddModule(new ModuleInfo() { ModuleName = "NHibernateModule", ModuleType = daoModule.AssemblyQualifiedName });
        //    //catalog.AddModule(new ModuleInfo() { ModuleName = "BusinessModule", ModuleType = businessModule.AssemblyQualifiedName });
        //    //catalog.AddModule(new ModuleInfo() { ModuleName = "UICoreModule", ModuleType = uiCoreModule.AssemblyQualifiedName });
        //    //catalog.AddModule(new ModuleInfo() { ModuleName = "UICoreViewModule", ModuleType = uiCoreViewModule.AssemblyQualifiedName });

        //    //DIR
        //    var catalog = new DirectoryModuleCatalog {ModulePath = @".\Modules"};
        //    catalog.Load();
        //    //Detached File XAML(Change Build mode to Content)
        //    //var catalogStream = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open);
        //    //var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogStream);
        //    //catalogStream.Dispose();

        //    //XAML(Change Build mode to Resource)
        //    //var catalog =
        //    //    Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri(
        //    //                                                                          "/LOB.UI.Core.View;component/Modularity/ModuleCatalog.xaml",
        //    //                                                                          UriKind.Relative));
        //    ModuleCatalog.AddModule();
        //    return catalog;
        //}

        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            AggregateCatalog.Catalogs.Add(new DirectoryCatalog("Modules"));
        }

        protected override DependencyObject CreateShell() { return Container.GetExportedValue<Shell>(); }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}