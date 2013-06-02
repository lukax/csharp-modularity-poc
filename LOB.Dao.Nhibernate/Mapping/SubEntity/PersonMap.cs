#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class PersonMap : BaseEntityMap<Person> {
        public PersonMap() {
            UseUnionSubclassForInheritanceMapping();
            References(x => x.Address).Cascade.All();
        }
    }
}