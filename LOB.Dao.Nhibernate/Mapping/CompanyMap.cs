#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping
{
    public class CompanyMap : SubclassMap<Company>
    {
        public CompanyMap()
        {
            HasMany(x => x.Stores)
                .Cascade.All();
        }
    }
}