#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class CategoryMap : BaseEntityMap<Category> {
        public CategoryMap() {
            Map(x => x.Name);
            Map(x => x.Detail);
        }
    }
}