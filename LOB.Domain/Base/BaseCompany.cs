#region Usings

using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain.Base
{
    public class BaseCompany : BaseEntity
    {
        public virtual string CorporateName { get; set; }
        public virtual string TradingName { get; set; }
        public virtual Address Address { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
    }
}