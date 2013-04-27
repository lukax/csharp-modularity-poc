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
            switch((NotificationType)value) {
                case NotificationType.Warning:
                    return new StaticResourceExtension();
                case NotificationType.Info:
                    return new Uri("");
                case NotificationType.Error:
                    return new Uri("");
                default:
                    throw new NotImplementedException();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }

        #endregion
    }
}