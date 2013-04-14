#region Usings

using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.UI.Core.Test.Infrastructure {
    [TestClass]
    public class ViewMappingTest {

        [TestMethod]
        public void Mapping() {
            var op1 = new ViewID {Type = ViewType.MessageTool};
            var op2 = ViewDictionary.ViewModels[op1];
            Assert.AreEqual(op1, op2);
        }

    }
}