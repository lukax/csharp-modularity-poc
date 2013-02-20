﻿#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls;
using LOB.UI.Core.ViewModel.Controls.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls
{
    [Export]
    public partial class AlterClientView : UserControl, ITabProp, IView
    {
        [ImportingConstructor]
        public AlterClientView(//AlterEntityViewModel<Employee> dataContext
            )
        {
            InitializeComponent();
           // dataContext.Entity = new Employee();
           // DataContext = dataContext;

            //Registrations
            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        public string Header
        {
            get { return "Alterar Cliente"; }
            set { }
        }

        public int? Index
        {
            get; 
            set;
        }

        public void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}