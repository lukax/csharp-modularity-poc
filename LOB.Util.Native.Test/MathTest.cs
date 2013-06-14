#region Usings

using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.Util.Native.Test {
    [TestClass]
    public class MathTest {
        [DllImport("..\\..\\..\\LOB.Util.Native\\Debug\\LOB.Util.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Power(double x, int y);

        [DllImport("..\\..\\..\\LOB.Util.Native\\Debug\\LOB.Util.Native.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double Random();

        [TestMethod]
        public void PowerTest() {
            double result = Power(5, 3);
            Assert.AreEqual(125, result);
        }

        [TestMethod]
        public void RandomTest() {
            double result1 = Random();
            double result2 = Random();
            Assert.AreNotEqual(result1, result2, "1: " + result1 + " 2: " + result2);
        }
    }
}