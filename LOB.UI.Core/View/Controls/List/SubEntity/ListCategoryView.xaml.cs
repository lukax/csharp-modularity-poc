﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    [Export]
    public partial class ListCategoryView : UserControl, IView, ITabProp
    {
        private string _header;

        public ListCategoryView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListCategoryView(BaseViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public BaseViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Códigos" : _header; }
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