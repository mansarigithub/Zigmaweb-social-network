using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigmaWeb.Common.Web
{
    public static class UriExtension
    {
        /// <summary>
        /// Returns first level domain of uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetTopLevelDomain(this Uri uri)
        {
            return uri.Host.Split('.').Last();
        }

        /// <summary>
        /// Returns second level domain of uri
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetSecondLevelDomain(this Uri uri)
        {
            var parts = uri.Host.Split('.');
            var partsCount = parts.Count();
            return string.Format("{0}.{1}", parts[partsCount - 2], parts[partsCount - 1]);
        }


    }
}
