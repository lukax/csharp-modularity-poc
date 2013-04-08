#region Usings

using System;
using System.Windows;
using LOB.Log;
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Modularity {
    public class Bootstrapper : UnityBootstrapper {

        protected override ILoggerFacade CreateLogger() { return new Logger(); }

        protected override IModuleCatalog CreateModuleCatalog() {
            //Reference
            //var catalog = new ModuleCatalog();
            //Type businessModule = typeof(LOB.Business.Module);
            //Type daoModule = typeof(LOB.Dao.Nhibernate.Module);
            //Type uiCoreModule = typeof(LOB.UI.Core.Module);
            //Type uiCoreViewModule = typeof(LOB.UI.Core.View.Module);
            //Type iLoggerModule = typeof (LOB.Log.Module);
            //catalog.AddModule(new ModuleInfo() { ModuleName = "LogModule", ModuleType = iLoggerModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "NHibernateModule", ModuleType = daoModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "BusinessModule", ModuleType = businessModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "UICoreModule", ModuleType = uiCoreModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "UICoreViewModule", ModuleType = uiCoreViewModule.AssemblyQualifiedName });

            //DIR
            //var catalog = new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
            //catalog.Load();

            //Detached File XAML(Change Build mode to Content)
            //var catalogStream = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open);
            //var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogStream);
            //catalogStream.Dispose();

            //XAML(Change Build mode to Resource)
            var catalog =
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(new Uri(
                                                                                      "/LOB.UI.Core.View;component/Modularity/ModuleCatalog.xaml",
                                                                                      UriKind.Relative));
            return catalog;
        }

        protected override void ConfigureContainer() {
            base.ConfigureContainer();
            RegisterTypeIfMissing(typeof(ILogger), typeof(Logger), true);
        }

        protected override DependencyObject CreateShell() {
            var main = Container.Resolve<Shell>();
            main.InitializeServices();
            return main;
        }

        protected override void InitializeShell() {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

    }
}