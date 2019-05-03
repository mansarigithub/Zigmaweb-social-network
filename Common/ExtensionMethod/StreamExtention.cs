using System;
using System.Drawing;
using System.IO;
namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class StreamExtention
    {
        public static byte[] ReadAllBytes(this Stream stream)
        {
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, (int)stream.Length);
            return buffer;
        }
    }
}