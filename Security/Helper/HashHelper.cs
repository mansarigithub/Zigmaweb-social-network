using System.Security.Cryptography;


namespace ZigmaWeb.Security.Helper
{
    public static class HashHelper
    {
        public static byte[] ComputeSha256Hash(string data)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(data);
            var hasher = new SHA256Managed();
            return hasher.ComputeHash(bytes);
        }
     }
}
