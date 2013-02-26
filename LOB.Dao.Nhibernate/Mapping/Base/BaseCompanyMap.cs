#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base
{
    public class BaseCompanyMap : BaseEntityMap<BaseCompany>
    {
        public BaseCompanyMap() {
            UseUnionSubclassForInheritanceMapping();
            Map(x => x.CorporateName);
            Map(x => x.TradingName);
            References(x => x.Address)
                .Cascade.All();
            References(x => x.ContactInfo)
                .Cascade.All();
        }
    }
}