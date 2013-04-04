#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#endregion

namespace LOB.Domain.SubEntity {
    [DefaultValue(Outro)]
    public enum UF {

        // ReSharper disable InconsistentNaming
        AC,
        AL,
        AP,
        AM,
        BA,
        CE,
        DF,
        ES,
        GO,
        MA,
        MT,
        MS,
        MG,
        PA,
        PB,
        PR,
        PE,
        PI,
        RJ,
        RN,
        RS,
        RO,
        RR,
        SC,
        SP,
        SE,
        TO,
        // ReSharper restore InconsistentNaming
        Outro

    }

    public static class UFDictionary {

        private static readonly Lazy<IDictionary<UF, string>> Lazy =
            new Lazy<IDictionary<UF, string>>(
                () =>
                new Dictionary<UF, string> {
                    {UF.AC, "Acre"},
                    {UF.AL, "Alagoas"},
                    {UF.AP, "Amapá"},
                    {UF.AM, "Amazonas"},
                    {UF.BA, "Bahia"},
                    {UF.CE, "Ceará"},
                    {UF.DF, "Brasília"},
                    {UF.ES, "Espírito Santo"},
                    {UF.GO, "Goiás"},
                    {UF.MA, "Maranhão"},
                    {UF.MT, "Mato Grosso"},
                    {UF.MS, "Mato Grosso do Sul"},
                    {UF.MG, "Minas Gerais"},
                    {UF.PA, "Pará"},
                    {UF.PB, "Paraíba"},
                    {UF.PR, "Paraná"},
                    {UF.PE, "Pernambuco"},
                    {UF.PI, "Piauí"},
                    {UF.RJ, "Rio de Janeiro"},
                    {UF.RN, "Rio Grande do Norte"},
                    {UF.RS, "Rio Grande do Sul"},
                    {UF.RO, "Rondônia"},
                    {UF.RR, "Roraima"},
                    {UF.SC, "Santa Catarina"},
                    {UF.SP, "São Paulo"},
                    {UF.SE, "Sergipe"},
                    {UF.TO, "Tocantins"},
                    {UF.Outro, ""},
                });

        public static IDictionary<UF, string> Ufs {
            get { return Lazy.Value; }
        }

    }

    public static class UFExtensions {

        public static UF ToUF(this string s) {
            UF parsed;
            if(s.Length == 2) return Enum.TryParse(s, out parsed) ? parsed : default(UF);
            return UFDictionary.Ufs.FirstOrDefault(x => x.Value.ToLower() == s.ToLower()).Key;
        }

        public static string ToLocalizedString(this UF uf) { return UFDictionary.Ufs[uf]; }

    }
}