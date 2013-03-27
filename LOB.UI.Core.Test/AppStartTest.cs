#region Usings

using LOB.UI.Core.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.UI.Core.Test {
    [TestClass]
    public class AppStartTest {
        [TestMethod]
        public void StartComposed() {
            var app = new App();
        }
    }
}