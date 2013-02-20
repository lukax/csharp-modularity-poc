using FluentNHibernate.Mapping;
using LOB.Domain.SubEntity;

namespace LOB.Dao.Nhibernate.Mapping.SubEntity
{
    public class CategoryMap : SubclassMap<Category>
    {
        public CategoryMap()
        {
        }
    }
}