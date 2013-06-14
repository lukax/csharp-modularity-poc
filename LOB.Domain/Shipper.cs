using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

namespace LOB.Domain {
    public class Shipper : LegalPerson {
        public IEnumerable<Shipment> Shipments { get; set; }
    }
}