#region Usings
using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class NaturalPersonMap : SubclassMap<NaturalPerson> {

        public NaturalPersonMap() {
            this.Map(x => x.FirstName);
            this.Map(x => x.LastName);
            this.Map(x => x.NickName);
            this.Map(x => x.BirthDate);
            this.Map(x => x.Cpf);
            this.Map(x => x.Rg);
            this.Map(x => x.RgUf);
        }

    }
}