#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel
{
    [Export]
    public class MainWindowViewModel : BaseViewModel
    {
        [ImportingConstructor]
        public MainWindowViewModel(IUnityContainer container)
        {
            _container = container;

            OpenTabCommand = new DelegateCommand(OpenTab);

            //Registrations
            //Messenger.IsDefault.Register<Product>(this, "UpdateCommand", OpenTab);
            //Messenger.IsDefault.Register<Employee>(this, "UpdateCommand", OpenTab);
        }

        public ICommand OpenTabCommand { get; set; }
        private IUnityContainer _container { get; set; }

        [Import]
        private INavigator _navigator { get; set; }


        //private void OpenTab<T>(T entity) where T : BaseEntity
        //{
        //    //Object item = ReferenceMap.ResolveView(entity);
        //    //Messenger.IsDefault.Send(item, "OpenTab");
        //}

        private void OpenTab(object newTabViewModel)
        {
            if (newTabViewModel is string)
                Messenger.Default.Send(_navigator.ResolveView(newTabViewModel.ToString()), "OpenTab");
            //Object item = ReferenceMap.ResolveView(newTabViewModel.ToString());
            //Messenger.IsDefault.Send(item, "OpenTab");
        }

        public override void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}