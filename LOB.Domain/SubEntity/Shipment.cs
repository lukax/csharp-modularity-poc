#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Shipment : BaseEntity, IEquatable<Shipment> {
        public ShipmentStatus Status { get; set; }
        public LegalPerson Shipper { get; set; }
        public Contact Contact { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime DeliverDate { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Service> Services { get; set; }
        #region Implementation of IEquatable<ShipmentInfo>

        public bool Equals(Shipment other) {
            try {
                return
                        base.Equals(other) &&
                        other.Status.Equals(Status) &&
                        other.Shipper.Equals(Shipper) &&
                        other.Contact.Equals(Contact) &&
                        other.ScheduleDate.Equals(ScheduleDate) &&
                        other.DeliverDate.Equals(DeliverDate) &&
                        other.Products.SequenceEqual(Products) &&
                        other.Services.SequenceEqual(Services);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }

    [Serializable]
    public enum ShipmentStatus {
        Active,
        Paused,
        Finished,
        Cancelled
    }
}