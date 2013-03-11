﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    [Export]
    public partial class ListCategoryBaseView : UserControl, IBaseView
    {
        private string _header;

        public ListCategoryBaseView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListCategoryBaseView(IListCategoryViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Categorys" : _header; }
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