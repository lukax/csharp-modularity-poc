#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base
{
    public class PersonMap : BaseEntityMap<Person>
    {
        public PersonMap() {
            UseUnionSubclassForInheritanceMapping();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.NickName);
            Map(x => x.BirthDate);
            HasMany(x => x.Address);
            References(x => x.ContactInfo);
            Map(x => x.Notes);
        }
    }
}