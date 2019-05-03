using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model.Business
{
    public class BusinessIntroducePM
    {
        [RequiredField]
        [LocalizedName("BusinessIntroduceText")]
        [StringLengthRange(10, 200)]
        public string Text { get; set; }
    }
}
