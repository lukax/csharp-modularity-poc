#region Usings
using FluentNHibernate.Mapping;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base {
    public abstract class BaseEntityMap<T> : ClassMap<T> where T : BaseEntity {

        protected BaseEntityMap() {
            this.Id(x => x.Id).UniqueKey("PK_" + typeof(T).Name).GeneratedBy.Guid();
            this.Map(x => x.Code).Not.Nullable().Generated.Insert();
        }

    }
}