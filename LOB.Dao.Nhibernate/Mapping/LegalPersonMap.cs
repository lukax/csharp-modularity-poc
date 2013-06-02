#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain.Base;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class LegalPersonMap : SubclassMap<LegalPerson> {
        public LegalPersonMap() {
            Map(x => x.CorporateName);
            Map(x => x.TradingName);
            Map(x => x.CNPJ);
            Map(x => x.InscEstadual);
            Map(x => x.InscMunicipal);
            Map(x => x.CNAEFiscal);
        }
    }
}