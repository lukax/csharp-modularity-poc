#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base {
    public class ServiceMap : BaseEntityMap<Service> {

        public ServiceMap() {
            UseUnionSubclassForInheritanceMapping();
            Map(x => x.Description);
            Map(x => x.Name);
        }

    }
}