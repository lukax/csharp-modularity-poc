#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain.SubEntity
{
    [Serializable]
    public class ShipmentInfo : Service
    {
        public virtual ShipmentStatus Status { get; set; }
        public virtual Address Address { get; set; }
        public virtual DateTime DeliverDate { get; set; }
        public virtual int DaySchedule { get; set; }
        public virtual IList<Product> Products { get; set; }
    }

    [Serializable]
    public enum ShipmentStatus
    {
        Active,
        Paused,
        Finished,
        Cancelled
    }
}