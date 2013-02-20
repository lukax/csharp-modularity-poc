using FluentNHibernate.Mapping;
using LOB.Domain;

namespace LOB.Dao.Nhibernate.Mapping
{
    public class SupplierMap : SubclassMap<Supplier>
    {
        public SupplierMap()
        {
        }
    }
}