#region Usings

using System.ComponentModel;
using System.Windows.Input;

#endregion

namespace MahApps.Metro.Controls
{
    /// <summary>
    ///     The minimum specification that a tile needs to support
    /// </summary>
    public interface IPanoramaTile
    {
        ICommand TileClickedCommand { get; }
    }

    public class PanoramaTile : INotifyPropertyChanged, IPanoramaTile
    {
        private string text;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                RaisePropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand TileClickedCommand
        {
            get { return null; }
        }

        /// <summary>
        ///     Raises the PropertyChanged event if needed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}