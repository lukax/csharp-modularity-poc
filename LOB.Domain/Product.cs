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
    public class Product : Merchandise, IEquatable<Product> {
        public ProductStatus Status { get; set; }
        public Category Category { get; set; }
        public int CodeBarras { get; set; }
        public int CodeNCM { get; set; }
        public int CFOP { get; set; }
        public byte[] Image { get; set; }
        public Stock Stock { get; set; }
        public IEnumerable<Company> AssociatedCompanies { get; set; }
        public IEnumerable<Order> AssociatedOrders { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        #region Implementation of IEquatable<Product>

        public bool Equals(Product other) {
            try {
                return
                        base.Equals(other) &&
                        other.Status.Equals(Status) &&
                        other.Category.Equals(Category) &&
                        other.CodeBarras.Equals(CodeBarras) &&
                        other.CodeNCM.Equals(CodeNCM) &&
                        other.CFOP.Equals(CFOP) &&
                        other.Image.Equals(Image) &&
                        other.Stock.Equals(Stock) &&
                        other.AssociatedCompanies.SequenceEqual(AssociatedCompanies) &&
                        other.AssociatedOrders.SequenceEqual(AssociatedOrders) &&
                        other.Suppliers.SequenceEqual(Suppliers);
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

    public static class ProductStatusExtension {
        public static ProductStatus ToProductStatus(this string s) { return default(ProductStatus); }
        public static string ToLocalizedString(this ProductStatus s) { return ""; }
    }
}