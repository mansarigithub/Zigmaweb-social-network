using System;
using System.Drawing;
using System.IO;
namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class ByteArrayExtention
    {
        public static Image ToImage(this byte[] bytes)
        {
            //using (MemoryStream ms = new MemoryStream(bytes))
            //{
            //    return Image.FromStream(ms);
            //}
            MemoryStream ms = new MemoryStream(bytes);

            return Image.FromStream(ms);
        }
    }
}