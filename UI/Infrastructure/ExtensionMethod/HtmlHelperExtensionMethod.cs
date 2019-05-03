using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.UI.Infrastructure.ExtensionMethod
{
    public static class HtmlHelperExtensionMethod
    {
        public static MvcHtmlString ValidationMessageFor2<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(htmlHelper.ValidationMessageFor(
                expression, "", new { @class = "help-block help-block-error" }).ToString());
        }

        public static MvcHtmlString ValidationMessageFor2<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string cssClass)
        {
            cssClass = $"help-block help-block-error {cssClass}";
            return MvcHtmlString.Create(htmlHelper.ValidationMessageFor(
                expression, "", new { @class = cssClass }).ToString());
        }

        public static MvcHtmlString LocalizedEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, bool addEmptyField = false, string optionLabel = null, object htmlAttributes = null)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var enumName = typeof(TEnum).Name;
            IEnumerable<TEnum> enumValues = System.Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            List<SelectListItem> listItems = new List<SelectListItem>();
            if (addEmptyField)
                listItems.Add(new SelectListItem { Text = string.Empty, Value = "" });
            enumValues.ForEach(value => {
                listItems.Add(new SelectListItem {
                    Text = $"{enumName}_{value}".Localize(),
                    Value = value.ToString(),
                    Selected = (value.Equals(metadata.Model))
                });
            });

            return htmlHelper.DropDownListFor(expression, listItems, optionLabel, htmlAttributes);
        }
    }
}