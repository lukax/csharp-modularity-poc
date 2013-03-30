#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class CommandMap : BaseEntityMap<Command> {

        public CommandMap() {
            this.Map(x => x.Name);
            this.Map(x => x.Parameter).CustomType("Serializable");
            this.Map(x => x.Task).CustomType("Serializable");
        }

    }
}