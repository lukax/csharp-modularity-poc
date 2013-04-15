#region Usings

using System;
using System.Diagnostics;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    [Serializable]
    public class LegalPerson : Person, IEquatable<LegalPerson> {
        public string CorporateName { get; set; }
        public string TradingName { get; set; }
        public string CNPJ { get; set; }
        public string InscEstadual { get; set; }
        public string InscMunicipal { get; set; }
        public string CNAEFiscal { get; set; }
        #region Implementation of IEquatable<LegalPerson>

        public bool Equals(LegalPerson other) {
            try {
                return base.Equals(other) && other.CorporateName.Equals(CorporateName) && other.TradingName.Equals(TradingName) &&
                       other.CNPJ.Equals(CNPJ) && other.InscEstadual.Equals(InscEstadual) && other.InscMunicipal.Equals(InscMunicipal) &&
                       other.CNAEFiscal.Equals(CNAEFiscal);
            } catch(NullReferenceException ex) {
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return false;
            }
        }

        #endregion
    }
}