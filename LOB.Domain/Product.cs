#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class Product : BaseEntity, IEquatable<Product> {
        public Category Category { get; set; }
        public ProductStatus Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
        #region Implementation of IEquatable<Product>

        public bool Equals(Product other) {
            try {
                return base.Equals(other) && other.Category.Equals(Category) && other.Status.Equals(Status) && other.CodBarras.Equals(CodBarras) &&
                       other.CodNCM.Equals(CodNCM) && other.CFOP.Equals(CFOP) && other.Image.Equals(Image) && other.UnitsInStock.Equals(UnitsInStock) &&
                       other.MaxUnitsOfStock.Equals(MaxUnitsOfStock) && other.MinUnitsOfStock.Equals(MinUnitsOfStock) &&
                       Equals(other.UnitCostPrice, UnitCostPrice) && Equals(other.UnitSalePrice, UnitSalePrice) &&
                       Equals(other.ProfitMargin, ProfitMargin) && other.QuantityPerUnit.Equals(QuantityPerUnit) &&
                       other.StockedStores.SequenceEqual(StockedStores) && other.Sales.SequenceEqual(Sales) &&
                       other.Suppliers.SequenceEqual(Suppliers) && other.ShipmentInfo.Equals(ShipmentInfo);
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
    public enum ProductStatus {
        Plenty,
        NotMany,
        Low,
        OutOfStorage,
        Discontinued
    }

    public static class ProductStatusExtensions {
        public static ProductStatus ToProductStatus(this string s) { return default(ProductStatus); }
        public static string ToLocalizedString(this ProductStatus s) { return ""; }
    }
}