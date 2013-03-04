#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class LegalPerson : Person
    {
        public virtual string CorporateName { get; set; }
        public virtual string TradingName { get; set; }
        public virtual int Cnpj { get; set; }
        public virtual int Iestadual { get; set; }
        public virtual int Imunicipal { get; set; }
        public virtual int CnaeFiscal { get; set; }
    }
}