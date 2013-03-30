#region Usings
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base {
    public class PersonMap : BaseEntityMap<Person> {

        public PersonMap() {
            this.UseUnionSubclassForInheritanceMapping();
            References(x => x.Address).Cascade.All();
            References(x => x.ContactInfo).Cascade.All();
            this.Map(x => x.Notes);
        }

    }
}