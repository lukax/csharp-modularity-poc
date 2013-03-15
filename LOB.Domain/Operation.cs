using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Domain.Base;

namespace LOB.Domain
{
    [Serializable]
    public class Operation :BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual object Parameter { get; set; }
        public virtual ICommand Command { get; set; }
    }
}
