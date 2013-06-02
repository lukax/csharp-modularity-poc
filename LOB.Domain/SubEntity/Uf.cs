#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

#endregion

namespace LOB.Domain.SubEntity {
    [Serializable]
    [DefaultValue(Outro)]
    public enum UF {
        // ReSharper disable InconsistentNaming
        AC = 12,
        AL = 27,
        AP = 16,
        AM = 13,
        BA = 29,
        CE = 23,
        DF = 53,
        ES = 32,
        GO = 52,
        MA = 21,
        MT = 51,
        MS = 50,
        MG = 31,
        PA = 15,
        PB = 25,
        PR = 41,
        PE = 26,
        PI = 22,
        RJ = 33,
        RN = 24,
        RS = 43,
        RO = 11,
        RR = 14,
        SC = 42,
        SP = 35,
        SE = 28,
        TO = 17,
        // ReSharper restore InconsistentNaming
        Outro = 0
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

    public static class UFExtension {
        public static UF ToUF(this string s) {
            UF parsed;
            if(s.Length == 2) return Enum.TryParse(s, out parsed) ? parsed : default(UF);
            return UFDictionary.Ufs.FirstOrDefault(x => x.Value.ToLower() == s.ToLower()).Key;
        }

        public static string ToLocalizedString(this UF uf) { return UFDictionary.Ufs[uf]; }

        public static IEnumerable<string> GetDistricts(this UF uf) {
            var ibgeCod = (int)uf;
            IEnumerable<string> contents = null;
            foreach(string file in Directory.EnumerateFiles("..//..//..//lib//MunIBGE", "*.txt")) if(file.Contains(ibgeCod.ToString(Thread.CurrentThread.CurrentCulture))) contents = File.ReadLines(file, Encoding.Default);
            //Faster than Regex.Replace(input, @"[\d-]", "");
            return contents != null ? contents.Select(content => content.Remove(0, 8)) : null;
        }
    }
}