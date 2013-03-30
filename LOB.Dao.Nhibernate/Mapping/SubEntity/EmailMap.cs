#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Dao.Nhibernate.Mapping.SubEntity {
    public class EmailMap : BaseEntityMap<Email> {

        public EmailMap() {
            this.Map(x => x.Value);
        }

    }
}