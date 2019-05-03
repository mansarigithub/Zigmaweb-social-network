using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZigmaWeb.Common.ExtensionMethod
{
    public static class HtmlExtention
    {
        public static string HighlightKeyWords(this string text, string keywords, bool fullMatch = true)
        {
            if (string.IsNullOrWhiteSpace(text) || keywords == string.Empty)
                return text;
            var words = keywords.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).Where(k => k.Length > 2);
            if (!fullMatch)
                return words.Select(word => word.Trim()).Aggregate(text,
                    (current, pattern) =>
                        Regex.Replace(current,
                            pattern,
                            $"<span style=\"background-color:{"Yellow"}\">{"$0"}</span>",
                            RegexOptions.IgnoreCase));

            return words.Select(word => "\\b" + word.Trim() + "\\b")
                .Aggregate(text, (current, pattern) =>
                    Regex.Replace(current,
                        pattern,
                        $"<span style=\"background-color:{"Yellow"}\">{"$0"}</span>",
                        RegexOptions.IgnoreCase));
        }
    }
}