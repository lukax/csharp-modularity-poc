using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOB.Core
{
    public class TrialMaker
    {
        private static int _currentRun = 1;
        private static string _registryPath = "Software\\LOBAPP";
        private static string _trialVersionKey = "TVS";
        private static string _fullVersionKey = "FVS";
        private const int TrailRuns = 30;

        public static bool IsLicenseValid()
        {
            //string key = RegistryUtility.GetFromRegistry(_registryPath, _trialVersionKey).ToString();
            return false;
        }
    }
}
