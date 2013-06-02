using System;
using System.Collections.Generic;
using LOB.Domain.Base;

namespace LOB.Domain.SubEntity {
    [Serializable]
    public class Stock : BaseEntity {
        public int CurrentUnits { get; set; }
        public int MinUnits { get; set; }
        public int MaxUnits { get; set; }
        public bool NeedReposition { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public Company AssociatedCompany { get; set; }
    }
}