#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class CommandMap : BaseEntityMap<Command> {
        public CommandMap() {
            Map(x => x.Name);
            Map(x => x.Parameter).CustomType("Serializable");
            Map(x => x.Task).CustomType("Serializable");
        }
    }
}