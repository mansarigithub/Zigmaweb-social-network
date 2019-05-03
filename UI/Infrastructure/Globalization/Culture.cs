using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ZigmaWeb.UI.Classes.Globalization
{
    public static class Culture
    {
        static List<CultureInfo> _supportedCultures;

        static Culture()
        {

        }

        public static List<CultureInfo> SupportedCultures
        {
            get
            {
                if(_supportedCultures == null)
                {
                    _supportedCultures = new List<CultureInfo>(3);
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("fa-IR")); // Persian (Iran)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("en-US")); // English (United States)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("ar-AE")); // Arabic (U.A.E.)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("az-Latn-AZ")); // Azeri (Latin, Azerbaijan)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("fr-FR")); // French (France)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("tr-TR")); //  Turkish (Turkey)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("de-DE")); // German (Germany)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("es-ES")); // Spanish (Spain)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("it-IT")); // Italian (Italy)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("ja-JP")); //Japanese (Japan)
                    _supportedCultures.Add(CultureInfo.GetCultureInfo("zh-CN")); //  Chinese (People's Republic of China)
                }

                return _supportedCultures;
            }
        }
    }
}