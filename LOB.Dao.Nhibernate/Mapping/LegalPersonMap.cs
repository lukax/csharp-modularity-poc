#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class LegalPersonMap : SubclassMap<LegalPerson> {

        public LegalPersonMap() {
            Map(x => x.CorporateName);
            Map(x => x.TradingName);
            Map(x => x.CNPJ);
            Map(x => x.Iestadual);
            Map(x => x.Imunicipal);
            Map(x => x.CNAEFiscal);
        }

    }
}