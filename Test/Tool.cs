using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZigmaWeb.DataAccess.Context;
using System.Linq;
using ZigmaWeb.Common.Drawing;
using ZigmaWeb.Common.Configuration;

namespace ZigmaWeb.Test
{
    [TestClass]
    public class Tool
    {
        [TestMethod]
        public void CreateThumbnails()
        {
            ZigmaWebContext c = new ZigmaWebContext();
            var dir = AppConfigurationManager.GetUserMediaDirectory();

            c.Medias.ToList().ForEach(m => {
                var path = $"z:\\public_html\\usermedia\\{m.FileName}";

            });
        }
    }
}
