#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class Company : BaseCompany
    {
        public virtual IList<Store> Stores { get; set; }
    }
}