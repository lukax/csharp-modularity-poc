#region Usings

using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.Util.Native.Test {
    [TestClass]
    public class MathTest {

        [DllImport("..\\..\\..\\LOB.Util.Native\\Debug\\LOB.Util.Native.dll",CallingConvention = CallingConvention.Cdecl)]
        public static extern double Power(double x, int y);

        [DllImport("..\\..\\..\\LOB.Util.Native\\Debug\\LOB.Util.Native.dll",CallingConvention = CallingConvention.Cdecl)]
        public static extern double Random();

        [TestMethod]
        public void PowerTest() {
            var result = Power(5, 3);
            Assert.AreEqual(125, result);
        }

        [TestMethod]
        public void RandomTest() { 
            var result1 = Random();
            var result2 = Random();
            Assert.AreNotEqual(result1, result2, "1: "+ result1 + " 2: "+ result2);
        }

    }
}