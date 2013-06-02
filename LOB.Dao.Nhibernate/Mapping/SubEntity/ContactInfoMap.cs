#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class ContactInfoMap : BaseEntityMap<ContactInfo> {
        public ContactInfoMap() {
            Map(x => x.Status);
            Map(x => x.Description);
            HasMany(x => x.PhoneNumbers).Cascade.All();
            HasMany(x => x.Emails).Cascade.All();
            Map(x => x.WebSite);
            Map(x => x.SpeakWith);
            Map(x => x.PS);
        }
    }
}