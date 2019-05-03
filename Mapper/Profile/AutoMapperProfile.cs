using System.Reflection;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Common.ExtensionMethod;

namespace ZigmaWeb.Mapper.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        protected override void Configure()
        {
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .ForEach(type => {
                    var attribute = type.GetCustomAttribute<ObjectMapperAttribute>();
                    if (attribute != null) {
                        MethodInfo methodInfo = type.GetMethod("CreateMap", BindingFlags.Static | BindingFlags.Public);
                        methodInfo.Invoke(null, new object[] { this });
                    }
                });
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }
    }

}
