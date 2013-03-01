﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    [Export]
    public partial class ListCustomerView : UserControl, ITabProp, IView
    {
        private string _header;

        public ListCustomerView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListCustomerView(ListClientViewModel dataContext)
            : this()
        {
            DataContext = dataContext;
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Clientes" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}