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
    public partial class AlterClientView : UserControl, ITabProp, IView
    {
        private string _header;
        private IUnityContainer _container;

        public AlterClientView()
        {
            InitializeComponent();

            Messenger.Default.Register<object>(DataContext, "SaveChangesCommand", o => Messenger.Default.Send("Cancel"));
        }

        [ImportingConstructor]
        public AlterClientView(AlterClientViewModel dataContext, IUnityContainer container)
            : this()
        {
            DataContext = dataContext;
            _container = container;

            AlterBaseEntity.DataContext = _container.Resolve<AlterBaseEntityViewModel<BaseEntity>>();
            AlterAddress.DataContext = _container.Resolve<AlterPersonViewModel>();
            AlterContactInfo.DataContext = _container.Resolve<AlterContactInfoViewModel>();
            AlterAddress.DataContext = _container.Resolve<AlterAddressViewModel>();
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

    public class UnityHelper
    {
        public static readonly DependencyProperty ContainerProperty = DependencyProperty.RegisterAttached("Container",
                                                                                                          typeof(
                                                                                                              IUnityContainer
                                                                                                              ),
                                                                                                          typeof(
                                                                                                              UnityHelper
                                                                                                              ),
                                                                                                          new FrameworkPropertyMetadata
                                                                                                              {
                                                                                                                  Inherits
                                                                                                                      =
                                                                                                                      true,
                                                                                                                  PropertyChangedCallback
                                                                                                                      =
                                                                                                                      (
                                                                                                                          obj,
                                                                                                                          e)
                                                                                                                      =>
                                                                                                                      {
                                                                                                                          var
                                                                                                                              container
                                                                                                                                  =
                                                                                                                                  e
                                                                                                                                      .NewValue
                                                                                                                                  as
                                                                                                                                  IUnityContainer;
                                                                                                                          if
                                                                                                                              (
                                                                                                                              container !=
                                                                                                                              null)
                                                                                                                          {
                                                                                                                              var
                                                                                                                                  element
                                                                                                                                      =
                                                                                                                                      obj
                                                                                                                                      as
                                                                                                                                      FrameworkElement;
                                                                                                                              container
                                                                                                                                  .BuildUp
                                                                                                                                  (obj
                                                                                                                                       .GetType
                                                                                                                                       (),
                                                                                                                                   obj,
                                                                                                                                   element ==
                                                                                                                                   null
                                                                                                                                       ? null
                                                                                                                                       : element
                                                                                                                                             .Name);
                                                                                                                          }
                                                                                                                      }
                                                                                                              });

        public static IUnityContainer GetContainer(DependencyObject obj)
        {
            return (IUnityContainer)obj.GetValue(ContainerProperty);
        }

        public static void SetContainer(DependencyObject obj, IUnityContainer value)
        {
            obj.SetValue(ContainerProperty, value);
        }
    }
}