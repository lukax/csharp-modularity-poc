#region Usings

using System;
using System.Windows.Input;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Command : BaseEntity
    {
        public string Name { get; set; }
        public object Parameter { get; set; }
        public ICommand Task { get; set; }
    }
}