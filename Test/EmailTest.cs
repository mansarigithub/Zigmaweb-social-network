using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZigmaWeb.Common.Messaging;

namespace ZigmaWeb.Test
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            EmailHelper.SendMailAsync("reg@zigmaweb.com", "mehrta.misc@live.com", "Yes Babe", "Body").Wait();
        }
    }
}
