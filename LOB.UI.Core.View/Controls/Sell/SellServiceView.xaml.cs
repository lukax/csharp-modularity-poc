﻿#region Usings

using System;
using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Core.View.Controls.Sell
{
    /// <summary>
    ///     Interaction logic for SellService.xaml
    /// </summary>
    public partial class SellServiceView : UserControl, IBaseView
    {
        public SellServiceView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel { get; set; }
        public string Header { get; set; }
        public int? Index { get; set; }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}