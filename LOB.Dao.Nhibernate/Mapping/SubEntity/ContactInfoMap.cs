#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity
{
    public class ContactInfoMap : BaseEntityMap<ContactInfo>
    {
        public ContactInfoMap() {
            Map(x => x.Status);
            HasMany(x => x.PhoneNumbers)
                .Cascade.All();
            HasMany(x => x.Emails)
                .Cascade.All();
            Map(x => x.WebSite);
            HasMany(x => x.SpeakWith);
            Map(x => x.Ps);
        }
    }
}