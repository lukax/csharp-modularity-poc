#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PhoneNumberMap : BaseEntityMap<PhoneNumber> {
        public PhoneNumberMap() {
            Map(x => x.Number);
            Map(x => x.PhoneNumberType);
            Map(x => x.Description);
        }
    }
}