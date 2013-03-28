#region Usings

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion

namespace LOB.Domain.Base
{
    [Serializable]
    public abstract class BaseNotifyChange : INotifyPropertyChanged
    {
        // Preventing some exceptions:
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Avisar mudança de valores numa property.
        /// </summary>
        /// <param name="propertyName">Implementando nova ferramenta do .NET 4.5, Atributo que passa o nome do metodo caller em string</param>
        protected internal void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}