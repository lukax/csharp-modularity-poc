#region Usings

using System.Windows;
using GalaSoft.MvvmLight.Threading;

#endregion

namespace LOB.UI.Core.View
{
    public sealed partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new Bootstrapper().Run();
        }
    }
}