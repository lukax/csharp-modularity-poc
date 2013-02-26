#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping
{
    public class LegalPersonMap : SubclassMap<LegalPerson>
    {
        public LegalPersonMap() {
            Map(x => x.Cnpj);
            Map(x => x.Ie);
        }
    }
}