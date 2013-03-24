using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOB.Domain.Logic
{
    public delegate ValidationResult ValidationDelegate(object sender, string propertyName);
}
