using System;
using System.Drawing;
using System.IO;
namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class ImageExtention
    {
        public static byte[] ToByteArray(this Image image, System.Drawing.Imaging.ImageFormat format)
        {
            byte[] arr; 
            using (MemoryStream ms = new MemoryStream()) {
                image.Save(ms, format);
                arr = ms.ToArray();
            }
            return arr;
        }

    }
}