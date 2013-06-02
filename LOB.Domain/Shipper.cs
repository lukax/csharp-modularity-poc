using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

namespace LOB.Domain
{
    public class Shipper : LegalPerson
    {
        public IEnumerable<Shipment> Shipments { get; set; }
    }
}
