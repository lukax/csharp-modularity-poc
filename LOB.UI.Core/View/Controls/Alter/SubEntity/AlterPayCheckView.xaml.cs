﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter.SubEntity
{
    [Export]
    public partial class AlterPayCheckView : UserControl, IView, ITabProp
    {
        private string _header;

        public AlterPayCheckView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterPayCheckView(AlterPayCheckViewModel viewModel)
            : this()
        {
            DataContext = viewModel;
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Paycheck" : _header; }
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