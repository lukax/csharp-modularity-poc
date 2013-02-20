#region Usings

using System;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain
{
    [Serializable]
    public class NaturalPerson : Person
    {
        public virtual int Cpf { get; set; }
        public virtual int Rg { get; set; }
        public virtual string RgUf { get; set; }
    }
}