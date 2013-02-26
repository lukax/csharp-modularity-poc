#region Usings

using System;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace MahApps.Metro.Controls
{
    public class Tile : Button
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof (string),
                                                                                              typeof (Tile),
                                                                                              new PropertyMetadata(
                                                                                                  default(string)));

        public static readonly DependencyProperty CountProperty = DependencyProperty.Register("Count", typeof (string),
                                                                                              typeof (Tile),
                                                                                              new PropertyMetadata(
                                                                                                  default(string)));

        public static readonly DependencyProperty KeepDraggingProperty = DependencyProperty.Register("KeepDragging",
                                                                                                     typeof (bool),
                                                                                                     typeof (Tile),
                                                                                                     new PropertyMetadata
                                                                                                         (true));

        public static readonly DependencyProperty TiltFactorProperty =
            DependencyProperty.Register("TiltFactor", typeof (int), typeof (Tile), new PropertyMetadata(5));

        public Tile() {
            DefaultStyleKey = typeof (Tile);
        }

        public string Title {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Count {
            get { return (string) GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public bool KeepDragging {
            get { return (bool) GetValue(KeepDraggingProperty); }
            set { SetValue(KeepDraggingProperty, value); }
        }

        public int TiltFactor {
            get { return (Int32) GetValue(TiltFactorProperty); }
            set { SetValue(TiltFactorProperty, value); }
        }
    }
}