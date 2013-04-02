#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Product : Service {

        public Category Category { get; set; }
        public ProductStatus Status { get; set; }
        public int CodBarras { get; set; }
        public int CodNCM { get; set; }
        public int CFOP { get; set; }
        public byte[] Image { get; set; }
        public int UnitsInStock { get; set; }
        public int MaxUnitsOfStock { get; set; }
        public int MinUnitsOfStock { get; set; }
        public double UnitCostPrice { get; set; }
        public double UnitSalePrice { get; set; }
        public double ProfitMargin { get; set; }
        public string QuantityPerUnit { get; set; }
        public IList<Store> StockedStores { get; set; }
        public IList<Sale> Sales { get; set; }
        public IList<Supplier> Suppliers { get; set; }
        public ShipmentInfo ShipmentInfo { get; set; }

    }

    [Serializable]
    public enum ProductStatus {

        Plenty,
        NotMany,
        Low,
        OutOfStorage,
        Discontinued

    }
}