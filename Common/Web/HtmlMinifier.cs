using System;
using System.Text.RegularExpressions;

namespace ZigmaWeb.Common.Web
{
    public static class HtmlMinifier
    {
        /// <summary>
        /// Minifies the given HTML string.
        /// </summary>
        /// <param name="htmlContents"> The html to minify.</param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string MinifyHtml(string htmlContents)
        {
            // Minify the string
            htmlContents = Regex.Replace(htmlContents, @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/", "");

            // Replace line comments
            htmlContents = Regex.Replace(htmlContents, @"// (.*?)\r?\n", "", RegexOptions.Singleline);

            // Replace spaces between quotes
            htmlContents = Regex.Replace(htmlContents, @"\s+", " ");

            // Replace line breaks
            htmlContents = Regex.Replace(htmlContents, @"\s*\n\s*", "\n");

            // Replace spaces between brackets
            htmlContents = Regex.Replace(htmlContents, @"\s*\>\s*\<\s*", "><");

            // single-line doctype must be preserved
            var firstEndBracketPosition = htmlContents.IndexOf(">", StringComparison.Ordinal);
            if (firstEndBracketPosition >= 0)
            {
                htmlContents = htmlContents.Remove(firstEndBracketPosition, 1);
                htmlContents = htmlContents.Insert(firstEndBracketPosition, ">");
            }

            return htmlContents.Trim();
        }
    }
}



