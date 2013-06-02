using System;
using System.Collections.Generic;
using LOB.Domain.Base;

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Stock : BaseEntity {
        public int Units { get; set; }
        public int Minunits { get; set; }
        public int MaxUnits { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Company AssociatedCompany { get; set; }
    }
}