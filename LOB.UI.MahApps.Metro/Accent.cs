#region Usings

using System;
using System.Windows;

#endregion

namespace MahApps.Metro
{
    public class Accent
    {
        public ResourceDictionary Resources;

        public Accent() {
        }

        public Accent(string name, Uri resourceAddress) {
            Name = name;
            Resources = new ResourceDictionary {Source = resourceAddress};
        }

        public string Name { get; set; }
    }
}