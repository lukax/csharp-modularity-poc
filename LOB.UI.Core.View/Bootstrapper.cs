using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
            return new TextLogger();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalogStream = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open);
            var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(catalogStream);
            catalogStream.Dispose();
            return catalog;
        }

        protected override DependencyObject CreateShell()
        {
            Thread.Sleep(500);
            var main = Container.Resolve<MainWindow>();
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
