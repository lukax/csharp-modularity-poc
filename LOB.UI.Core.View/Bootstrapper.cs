using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LOB.Log;
using LOB.UI.Core.ViewModel;
using LOB.UI.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace LOB.UI.Core.View
{
    public class Bootstrapper : UnityBootstrapper
    {

        public Bootstrapper()
        {
        }

        protected override Microsoft.Practices.Prism.Logging.ILoggerFacade CreateLogger()
        {
            return new Logger();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //Reference
            //var catalog = new ModuleCatalog();
            //Type businessModule = typeof(LOB.Business.BusinessModule);
            //Type daoModule = typeof(LOB.Dao.Nhibernate.BusinessModule);
            //Type uiCoreModule = typeof(LOB.UI.Core.BusinessModule);
            //Type uiCoreViewModule = typeof(LOB.UI.Core.View.BusinessModule);
            //catalog.AddModule(new ModuleInfo() { ModuleName = "BusinessModule", ModuleType = businessModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "NHibernateModule", ModuleType = daoModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "UICoreModule", ModuleType = uiCoreModule.AssemblyQualifiedName });
            //catalog.AddModule(new ModuleInfo() { ModuleName = "UICoreViewModule", ModuleType = uiCoreViewModule.AssemblyQualifiedName });
            
            //DIR
            //var catalog = new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
            //catalog.Load();
            
            //XAML
            var catalogStream = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open);
            var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogStream);
            catalogStream.Dispose();
            return catalog;
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override DependencyObject CreateShell()
        {
            Thread.Sleep(500);
            var main = Container.Resolve<ShellWindow>();
            return main;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window) this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
