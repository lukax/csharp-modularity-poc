#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PersonMap : BaseEntityMap<Person> {
        public PersonMap() {
            UseUnionSubclassForInheritanceMapping();
            References(x => x.Address).Cascade.All();
            References(x => x.ContactInfo).Cascade.All();
            Map(x => x.Notes);
        }
    }
}