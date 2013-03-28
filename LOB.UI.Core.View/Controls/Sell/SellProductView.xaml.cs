﻿#region Usings

using System;
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Sell
{
    public partial class SellProductView : UserControl, IBaseView
    {
        public SellProductView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel { get; set; }

        public string Header
        {
            get { return Strings.Command_SellProduct; }
        }

        public int Index { get; set; }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public OperationType OperationType
        {
            get { return OperationType.SellProduct; }
        }
    }
}