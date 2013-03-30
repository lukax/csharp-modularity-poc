#region Usings
using System;
using System.Collections.Generic;

#endregion

namespace LOB.Domain.SubEntity {
    public enum UfBr {

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
        TO

    }

    public static class UfBrDictionary {

        private static readonly Lazy<IDictionary<UfBr, string>> Lazy =
            new Lazy<IDictionary<UfBr, string>>(
                () =>
                new Dictionary<UfBr, string> {
                    {UfBr.AC, "Acre"},
                    {UfBr.AL, "Alagoas"},
                    {UfBr.AP, "Amapá"},
                    {UfBr.AM, "Amazonas"},
                    {UfBr.BA, "Bahia"},
                    {UfBr.CE, "Ceará"},
                    {UfBr.DF, "Brasília"},
                    {UfBr.ES, "Espírito Santo"},
                    {UfBr.GO, "Goiás"},
                    {UfBr.MA, "Maranhão"},
                    {UfBr.MT, "Mato Grosso"},
                    {UfBr.MS, "Mato Grosso do Sul"},
                    {UfBr.MG, "Minas Gerais"},
                    {UfBr.PA, "Pará"},
                    {UfBr.PB, "Paraíba"},
                    {UfBr.PR, "Paraná"},
                    {UfBr.PE, "Pernambuco"},
                    {UfBr.PI, "Piauí"},
                    {UfBr.RJ, "Rio de Janeiro"},
                    {UfBr.RN, "Rio Grande do Norte"},
                    {UfBr.RS, "Rio Grande do Sul"},
                    {UfBr.RO, "Rondônia"},
                    {UfBr.RR, "Roraima"},
                    {UfBr.SC, "Santa Catarina"},
                    {UfBr.SP, "São Paulo"},
                    {UfBr.SE, "Sergipe"},
                    {UfBr.TO, "Tocantins"}
                });

        public static IDictionary<UfBr, string> Ufs {
            get { return Lazy.Value; }
        }

    }
}