﻿using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain.SubEntity;

namespace LOB.Dao.Nhibernate.Mapping.SubEntity
{
    public class PhoneNumberMap : BaseEntityMap<PhoneNumber>
    {
        public PhoneNumberMap()
        {
            Map(x => x.Number);
            Map(x => x.NumberType);
            Map(x => x.Description);
        }
    }
}