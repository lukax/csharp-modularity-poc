#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class ShipmentInfo : Service, IEquatable<ShipmentInfo> {

        public Shipper Shipper { get; set; }
        public ShipmentStatus Status { get; set; }
        public Address Address { get; set; }
        public DateTime DeliverDate { get; set; }
        public string DaySchedule { get; set; }
        public IList<Product> Products { get; set; }
        #region Implementation of IEquatable<ShipmentInfo>

        public bool Equals(ShipmentInfo other) {
            try {
                return base.Equals(other) && other.Shipper.Equals(Shipper) && other.Status.Equals(Status) && other.Address.Equals(Address) &&
                       other.DeliverDate.Equals(DeliverDate) && other.DaySchedule.Equals(DaySchedule) && other.Products.SequenceEqual(Products);
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