#region Usings

using System;
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
    public partial class AlterProductView : UserControl, ITabProp, IView
    {
        [ImportingConstructor]
        public AlterProductView(AlterEntityViewModel<Product> dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;

            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        public string Header
        {
            get { return "Alterar Produto"; }
            set { }
        }

        public int? Index
        {
            get { return ((AlterEntityViewModel<Product>) DataContext).CancelIndex; }
            set { ((AlterEntityViewModel<Product>) DataContext).CancelIndex = value; }
        }

        public void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}