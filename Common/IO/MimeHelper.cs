using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigmaWeb.Common.IO
{
    public static class MimeHelper
    {
        public static string GetMime(string fileExtension)
        {
            fileExtension = fileExtension.ToLower();
            switch (fileExtension) {
                case "jpeg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                default:
                    return "unknown";
            }
        }
    }
}
