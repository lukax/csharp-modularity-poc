#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Service.Contract.DCO {

    public class Category : Base.BaseEntity {
        public string Name { get; set; }
        public string Detail { get; set; }
    }
}