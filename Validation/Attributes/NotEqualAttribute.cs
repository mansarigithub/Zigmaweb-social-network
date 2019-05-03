//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace ZigmaWeb.Validation.Attributes
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
//    public class NotEqualAttribute : ValidationAttribute, IClientValidatable
//    {
//        private const string DefaultErrorMessage = "The value of {0} cannot be the same as the value of the {1}.";

//        public string OtherProperty { get; private set; }

//        public NotEqualAttribute(string otherProperty) : base(DefaultErrorMessage)
//        {
//            OtherProperty = otherProperty;
//        }

//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            if (value != null) {
//                var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(OtherProperty);

//                var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

//                if (value.Equals(otherPropertyValue)) {
//                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
//                }
//            }

//            return ValidationResult.Success;

//        }

//        IEnumerable<ModelClientValidationRule> IClientValidatable.GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
//        {
//            var rule = new ModelClientValidationRule()
//            {
//                ValidationType = "unlike",
//                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
//            };

//            rule.ValidationParameters.Add("otherproperty", OtherProperty);

//            yield return rule;
//        }

//        public override string FormatErrorMessage(string name)
//        {
//            return string.Format(ErrorMessageString, name);
//        }
//    }
//}
