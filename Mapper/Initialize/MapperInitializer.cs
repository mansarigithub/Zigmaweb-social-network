using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.Mapper.Profile;

namespace ZigmaWeb.Mapper.Initialize
{
    public static class MapperInitializer
    {
        public static void InitializModule()
        {
            AutoMapper.Mapper.Initialize(config =>
            {
                config.AddProfile<AutoMapperProfile>();
                //config.DisableConstructorMapping();
            });
        }
    }
}
