using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.Base;

namespace LOB.Domain
{
    public class Command : BaseEntity
    {
        public IDictionary<string, Command> Map { get; set; }
    }
}
