using System;
using System.ComponentModel.DataAnnotations;
using LOB.Core.Localization;
using LOB.Domain.Monetary;
using LOB.Domain.Util;

namespace LOB.Domain.Base {
    [Serializable]
    public abstract class Merchandise : BaseEntity {
        [Required(ErrorMessageResourceName = "Notification_Field_Required", ErrorMessageResourceType = typeof(Strings))]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Unit { get; set; }
        public SimpleMoney UnitCostPrice { get; set; }
        public SimpleMoney UnitSalePrice { get; set; }
        public float ProfitMargin { get; set; }
    }
}