#region Usings

using System.Windows;

#endregion

namespace MahApps.Metro.Controls
{
    public class RangeSelectionChangedEventArgs : RoutedEventArgs
    {
        internal RangeSelectionChangedEventArgs(long newRangeStart, long newRangeStop)
        {
            NewRangeStart = newRangeStart;
            NewRangeStop = newRangeStop;
        }

        internal RangeSelectionChangedEventArgs(RangeSlider slider)
            : this(slider.RangeStartSelected, slider.RangeStopSelected)
        {
        }

        public long NewRangeStart { get; set; }
        public long NewRangeStop { get; set; }
    }
}