using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.Mapper.Initialize;
using ZigmaWeb.Validation.Initialize;

namespace ZigmaWeb.UI
{
    public static class ApplicationInitializer
    {
        public static void Initialize()
        {
            MapperInitializer.InitializModule();
            ValidationInitializer.InitializModule();
        }
    }
}