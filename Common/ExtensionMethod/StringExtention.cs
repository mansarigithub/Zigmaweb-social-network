using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class StringExtention
    {
        public static String ToHtml(this string str)
        {
            str = str.Replace(Environment.NewLine, "<br/>");
            return str.Replace("\n", "<br/>");
        }

        public static String MakeShort(this string str, int maxLength)
        {
            return str.Length <= maxLength ?
                str :
                $"{str.Substring(0, maxLength)}...";
        }
    }
}