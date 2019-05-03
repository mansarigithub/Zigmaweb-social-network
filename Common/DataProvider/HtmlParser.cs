using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using HtmlAgilityPack;

namespace ZigmaWeb.Common.DataProvider
{
    public static class HtmlParser
    {
        public static string Parse(string htmlString)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            // begin link nodes
            // modify important attribues
            var linkNodes = doc.DocumentNode.SelectNodes("//a");

            if (linkNodes != null)
            {
                foreach (var linkNode in linkNodes)
                {
                    linkNode.SetAttributeValue("rel", "nofollow");
                    linkNode.SetAttributeValue("target", "_blank");
                }
            }
            // end modify link nodes


            TextWriter txtWriter = new StringWriter();
            doc.Save(txtWriter);

            return txtWriter.ToString();
        }
    }
}