
namespace Common
{
    public static class StringExtension
    {
        public static string Localize(this string str)
        {
            string localizedString;
            try
            {
                localizedString = Common.Properties.Resources.ResourceManager.GetString(str);
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