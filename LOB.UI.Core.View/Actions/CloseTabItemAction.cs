#region Usings

using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

#endregion

namespace LOB.UI.Core.View.Actions
{
    public class CloseTabItemAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty TabControlProperty = 
            DependencyProperty.Register("TabControl",
                                        typeof (TabControl),
                                        typeof (CloseTabItemAction),
                                        new PropertyMetadata(default(TabControl)));

        public static readonly DependencyProperty TabItemProperty = 
            DependencyProperty.Register("TabItem",
                                        typeof (TabItem),
                                        typeof (CloseTabItemAction),
                                        new PropertyMetadata(default(TabItem)));

        public TabControl TabControl
        {
            get { return (TabControl) GetValue(TabControlProperty); }
            set { SetValue(TabControlProperty, value); }
        }

        public TabItem TabItem
        {
            get { return (TabItem) GetValue(TabItemProperty); }
            set { SetValue(TabItemProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            if (TabControl.Items.Contains(TabItem))
            {
                TabControl.Items.Remove(TabItem);

            }
            else
            {
                TabControl.Items.Remove(TabItem.Content);
            }
        }
    }
}