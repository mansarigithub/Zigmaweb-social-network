using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.Validation.Initialize
{
    public static class ValidationInitializer
    {
        public static void InitializModule()
        {
            InitializCustomValidationAttributes();
        }

        private static void InitializCustomValidationAttributes()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(RequiredFieldAttribute), typeof(RequiredAttributeAdapter));

            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(StringLengthRangeAttribute), typeof(System.Web.Mvc.StringLengthAttributeAdapter));

        }
    }
}
