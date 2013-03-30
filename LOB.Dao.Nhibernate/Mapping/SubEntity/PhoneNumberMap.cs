#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PhoneNumberMap : BaseEntityMap<PhoneNumber> {

        public PhoneNumberMap() {
            this.Map(x => x.Number);
            this.Map(x => x.PhoneNumberType);
            this.Map(x => x.Description);
        }

    }
}