#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base
{
    public class PersonMap : BaseEntityMap<Person>
    {
        public PersonMap()
        {
            UseUnionSubclassForInheritanceMapping();
            HasMany(x => x.Address);
            References(x => x.ContactInfo);
            Map(x => x.Notes);
        }
    }
}