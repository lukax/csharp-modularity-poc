#region Usings

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterCustomerView : UserControl, ITabProp, IView
    {
        private string _header;
        private IUnityContainer _container;

        public AlterCustomerView()
        {
            InitializeComponent();

            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        [ImportingConstructor]
        public AlterCustomerView(AlterCustomerViewModel dataContext, IUnityContainer container, INavigator navigator)
            : this()
        {
            DataContext = dataContext;
            _container = container;

            if (navigator.PromptUser("YES => NATURAL PERSON \n NO => LEGAL PERSON"))
                TabAlterBaseEntity.DataContext = _container.Resolve<AlterNaturalPersonViewModel>();
            else
                TabAlterBaseEntity.DataContext = _container.Resolve<AlterLegalPersonViewModel>();

            //TODO: Legal or natural based on selection
            //TabAlterBaseEntity.DataContext = _container.Resolve<AlterBaseEntityViewModel<BaseEntity>>();
            //TabAlterPersonDetails.DataContext = _container.Resolve<AlterPersonViewModel<Person>>();
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Cliente" : _header; }
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