using ZigmaWeb.Common.Enum;
using ZigmaWeb.Mapper.Attributes;
using ZigmaWeb.Mapper.Profile;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.Mapper.Core
{
    [ObjectMapper]
    public static class MessageMapper
    {
        public static void CreateMap(AutoMapperProfile profile)
        {
            profile.CreateMap<Message, MessagePM>()
                  .ForMember(pm => pm.SenderFullName, opt => opt.Ignore())
                  .ForMember(pm => pm.ReceiverFullName, opt => opt.Ignore())
                  .ForMember(pm => pm.CreateDateString, opt => opt.Ignore());
            profile.CreateMap<MessagePM, Message>();
        }

        public static Message GetMessage(this MessagePM messagePM)
        {
            return AutoMapper.Mapper.Map<MessagePM, Message>(messagePM);
        }

        public static MessagePM GetMessagePM(this Message message)
        {
            return AutoMapper.Mapper.Map<Message, MessagePM>(message);
        }
    }
}
