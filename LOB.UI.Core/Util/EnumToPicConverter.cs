#region Usings

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using LOB.Domain.Logic;

#endregion

namespace LOB.UI.Core.Util {
    public class EnumToPicConverter : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch((NotificationState)value) {
                case NotificationState.Warning:
                    return new StaticResourceExtension();
                case NotificationState.Info:
                    return new Uri("");
                case NotificationState.Error:
                    return new Uri("");
                default:
                    throw new NotImplementedException();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }

        #endregion
    }
}