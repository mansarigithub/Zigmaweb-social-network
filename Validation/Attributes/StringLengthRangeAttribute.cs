using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.Validation.Attribute
{
    public class StringLengthRangeAttribute : System.ComponentModel.DataAnnotations.StringLengthAttribute
    {
        public StringLengthRangeAttribute(int minimumLength, int maximumLength, string errorMessageResourceName = "InvalidStringLength")
            : base(maximumLength)
        {
            this.MinimumLength = minimumLength;
            this.ErrorMessageResourceName = errorMessageResourceName;
            this.ErrorMessageResourceType = typeof(ZigmaWeb.Localization.Resources);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("MsgStringLengthRange".Localize(), name, MinimumLength, MaximumLength );
        }
    }
}
