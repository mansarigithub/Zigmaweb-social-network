using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZigmaWeb.DataAccess.Seed;
using ZigmaWeb.DataAccess.Context;
using System.Linq;
using System.Data.Entity;
using System.Globalization;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Model.Domain.Core;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SeedDatabaseAtLevel1()
        {
            DatabaseInitializer_Level1.Initialize();
        }

        [TestMethod]
        public void MiscTest()
        {
            ZigmaWebContext context = new ZigmaWebContext();
            context.Medias.ToList().ForEach(m => m.CreateDate = DateTime.Now);
            context.SaveChanges();
        }
    }
}
