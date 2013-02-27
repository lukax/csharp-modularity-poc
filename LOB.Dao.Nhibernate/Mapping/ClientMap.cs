#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping
{
    public class ClientMap : BaseEntityMap<Client>
    {
        public ClientMap()
        {
            HasManyToMany(x => x.ClientOf);
            Map(x => x.Status);
            HasMany(x => x.BoughtHistory);
            References(x => x.Person)
                .Cascade.All();
        }
    }
}