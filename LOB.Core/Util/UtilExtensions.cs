#region Usings
using System.Collections.Generic;
using System.Linq;

#endregion

namespace LOB.Core.Util {
    public static class UtilExtensions {

        public static IList<StringWrapper> Wrap(this IList<string> s) {
            return s.Select(variable => new StringWrapper(variable)).ToList();
        }

        public static StringWrapper Wrap(this string s) {
            return new StringWrapper(s);
        }

    }
}