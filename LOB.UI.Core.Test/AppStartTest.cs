#region Usings

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.UI.Core.Test
{
    [TestClass]
    public class AppStartTest
    {
        [TestMethod]
        public void StartComposed() {
            var app = new App();
        }
    }
}