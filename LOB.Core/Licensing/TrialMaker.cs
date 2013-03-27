namespace LOB.Core.Licensing {
    public class TrialMaker {
        private const int TrailRuns = 30;
        private static int _currentRun = 1;
        private static string _registryPath = "Software\\LOBAPP";
        private static string _trialVersionKey = "TVS";
        private static string _fullVersionKey = "FVS";

        public static bool IsLicenseValid() {
            //string key = RegistryUtility.GetFromRegistry(_registryPath, _trialVersionKey).ToString();
            return false;
        }
    }
}