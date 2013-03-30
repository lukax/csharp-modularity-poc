#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class ContactInfoMap : BaseEntityMap<ContactInfo> {

        public ContactInfoMap() {
            this.Map(x => x.Status);
            HasMany(x => x.PhoneNumbers).Cascade.All();
            HasMany(x => x.Emails).Cascade.All();
            this.Map(x => x.WebSite);
            this.Map(x => x.SpeakWith);
            this.Map(x => x.Ps);
        }

    }
}