#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping
{
    public class SupplierMap : SubclassMap<Supplier>
    {
        public SupplierMap() {
        }
    }
}