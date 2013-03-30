#region Usings
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.Base {
    public class ServiceMap : BaseEntityMap<Service> {

        public ServiceMap() {
            this.UseUnionSubclassForInheritanceMapping();
            this.Map(x => x.Description);
            this.Map(x => x.Name);
        }

    }
}