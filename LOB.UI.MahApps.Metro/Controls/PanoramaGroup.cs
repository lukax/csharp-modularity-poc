#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

#endregion

namespace MahApps.Metro.Controls
{
    /// <summary>
    ///     Represents a grouping of tiles
    /// </summary>
    public class PanoramaGroup : INotifyPropertyChanged
    {
        public PanoramaGroup(string header, ICollectionView tiles)
        {
            Header = header;
            Tiles = tiles;
        }

        public PanoramaGroup(string header, IEnumerable<object> tiles)
        {
            Header = header;
            Tiles = CollectionViewSource.GetDefaultView(tiles);
        }

        public PanoramaGroup(string header)
        {
            Header = header;
        }

        public string Header { get; private set; }
        public ICollectionView Tiles { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetSource(IEnumerable<object> tiles)
        {
            Tiles = CollectionViewSource.GetDefaultView(tiles);
            OnPropertyChanged("Tiles");
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}