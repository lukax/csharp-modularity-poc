#region Usings

using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.UI.Core.View.Actions {
    public class CloseTabItemAction : TriggerAction<DependencyObject> {
        public static readonly DependencyProperty TabControlProperty = DependencyProperty.Register("TabControl", typeof(TabControl),
                                                                                                   typeof(CloseTabItemAction),
                                                                                                   new PropertyMetadata(default(TabControl)));

        public static readonly DependencyProperty TabItemProperty = DependencyProperty.Register("TabItem", typeof(TabItem), typeof(CloseTabItemAction),
                                                                                                new PropertyMetadata(default(TabItem)));

        public TabControl TabControl {
            get { return (TabControl)GetValue(TabControlProperty); }
            set { SetValue(TabControlProperty, value); }
        }

        public TabItem TabItem {
            get { return (TabItem)GetValue(TabItemProperty); }
            set {
                SetValue(TabItemProperty, value);
                //var view = value as IBaseIuiComponent;
                //if(view != null) //TODO: Get proper Index and assign to view
                //var innerView = value.Content as IBaseIuiComponent;
                //if(innerView != null)
            }
        }

        public static IServiceLocator Container { get; set; }

        protected override void Invoke(object parameter) {
            if(TabControl.Items.Contains(TabItem))
                if(Container != null) {
// ReSharper disable SuspiciousTypeConversion.Global
                    var view = TabItem as IBaseView;
// ReSharper restore SuspiciousTypeConversion.Global
                    var region = Container.GetInstance<IRegionAdapter>();
                    if(view != null) region.RemoveView(view.ViewID, RegionName.TabRegion);
                }
                else TabControl.Items.Remove(TabItem);
            else if(TabControl.Items.Contains(TabItem.Content))
                if(Container != null) {
                    var view = TabItem.Content as IBaseView;
                    var region = Container.GetInstance<IRegionAdapter>();
                    if(view != null) region.RemoveView(view.ViewID, RegionName.TabRegion);
                }
                else TabControl.Items.Remove(TabItem.Content);
        }
    }
}