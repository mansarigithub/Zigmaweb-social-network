using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.Validation.Attribute
{
    public class EnforceTrueAttribute : ValidationAttribute, IClientValidatable
    {
        public EnforceTrueAttribute(string errorMessageResourceName)
        {
            ErrorMessageResourceName = errorMessageResourceName;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool))
                throw new InvalidOperationException("Can only be used on boolean properties.");
            return (bool)value;
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessageResourceName.Localize();
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            yield return new ModelClientValidationRule {
                ErrorMessage = ErrorMessageResourceName.Localize(),
                ValidationType = "enforcetrue"
            };
        }
    }
}
