using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.Security.Helper;

namespace ZigmaWeb.Security.ExtensionMethod
{
    public static class StringExtension
    {
        public static byte[] ComputeSha256Hash(this string str)
        {
            return HashHelper.ComputeSha256Hash(str);
        }
    }
}
