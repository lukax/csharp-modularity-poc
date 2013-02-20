#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class LegalPerson : Person
    {
        public virtual int Cnpj { get; set; }
        public virtual int Ie { get; set; }
        public virtual Company Company { get; set; }
    }
}