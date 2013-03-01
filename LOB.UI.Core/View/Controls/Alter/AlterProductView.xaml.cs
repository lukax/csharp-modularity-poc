﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterProductView : UserControl, ITabProp, IView
    {
        private string _header;
        private INavigator _navigator;

        public AlterProductView()
        {
            InitializeComponent();
        }

        public AlterProductViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
                this.TabAlterBaseEntityView.DataContext = value;
            }
        }

        [ImportingConstructor]
        public AlterProductView(AlterProductViewModel viewModel, INavigator navigator)
            : this()
        {
            _navigator = navigator;
            ViewModel = viewModel;
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Produto" : _header; }
            set { _header = value; }
        }

        public int? Index
        {
            get { return ((AlterBaseEntityViewModel<Product>)DataContext).CancelIndex; }
            set { ((AlterBaseEntityViewModel<Product>)DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        public void Refresh()
        {
        }
    }
}