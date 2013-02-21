#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Product : Service
    {
        public virtual Category Category { get; set; }
        public virtual ProductStatus Status { get; set; }
        public virtual int UnitsInStock { get; set; }
        public virtual double UnitCostPrice { get; set; }
        public virtual double UnitSalePrice { get; set; }
        public virtual double ProfitMargin { get; set; }
        public virtual string QuantityPerUnity { get; set; }
        public virtual IList<Store> StockedStores { get; set; }
        public virtual IList<Sale> Sales { get; set; }
        public virtual IList<Supplier> Suppliers { get; set; }
        public virtual ShipmentInfo ShipmentInfo { get; set; }
    }

    [Serializable]
    public enum ProductStatus
    {
        Plenty,
        NotMany,
        Low,
        OutOfStorage,
        Discontinued
    }
}