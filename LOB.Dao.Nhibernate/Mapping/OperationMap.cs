using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

namespace LOB.Dao.Nhibernate.Mapping
{
    public class OperationMap : BaseEntityMap<Operation>
    {
        public OperationMap()
        {
            Map(x => x.Name);
            Map(x => x.Parameter).CustomType("Serializable");
            Map(x => x.Command).CustomType("Serializable");
        }
    }
}
