﻿#region Usings

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
        public MainWindowViewModel(IUnityContainer container, INavigator navigator) {
            _container = container;
            _navigator = navigator;

            OpenTabCommand = new DelegateCommand(OpenTab);
        }

        public ICommand OpenTabCommand { get; set; }
        private IUnityContainer _container { get; set; }
        private INavigator _navigator { get; set; }

        private void OpenTab(object arg) {
            Messenger.Default.Send(_navigator.ResolveView(arg.ToString()).GetView, "OpenTab");
        }

        public override void InitializeServices() {
        }

        public override void Refresh() {
        }
    }
}