#region Usings

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#endregion

namespace LOB.Core.Util {
    public static class ImageUtility {

        public static byte[] ToBytes(this Image imageIn) {
            var ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ToImage(this byte[] byteArrayIn) {
            var ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

    }
}