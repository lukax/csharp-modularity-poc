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
        public virtual string Name { get; set; }
        public virtual object Parameter { get; set; }
        public virtual ICommand Task { get; set; }
    }
}