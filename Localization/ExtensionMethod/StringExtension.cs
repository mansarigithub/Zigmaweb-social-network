
namespace ZigmaWeb.Localization.ExtensionMethod
{
    public static class StringExtension
    {
        public static string Localize(this string str)
        {
            string localizedString;
            try
            {
                localizedString = Resources.ResourceManager.GetString(str);
                if (localizedString == null)
                    localizedString = str;
            }
            catch
            {
                localizedString = str; 
            }
            return localizedString;
        }
    }
}