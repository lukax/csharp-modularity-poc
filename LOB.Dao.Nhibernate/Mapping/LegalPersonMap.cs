#region Usings
using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class LegalPersonMap : SubclassMap<LegalPerson> {

        public LegalPersonMap() {
            this.Map(x => x.CorporateName);
            this.Map(x => x.TradingName);
            this.Map(x => x.Cnpj);
            this.Map(x => x.Iestadual);
            this.Map(x => x.Imunicipal);
            this.Map(x => x.CnaeFiscal);
        }

    }
}