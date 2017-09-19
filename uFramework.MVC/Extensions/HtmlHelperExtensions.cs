using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace uFramework.MVC.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString IdFor<TModel, TProperty>(
            this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return Id(htmlHelper, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString IdFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return Id(htmlHelper, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString Id(
            this HtmlHelper htmlHelper, string name)
        {
            return MvcHtmlString.Create(htmlHelper.AttributeEncode(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(name)));
        }

        public static MvcHtmlString NameFor<TModel, TProperty>(
            this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return Name(htmlHelper, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString NameFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return Name(htmlHelper, ExpressionHelper.GetExpressionText(expression));
        }

        public static MvcHtmlString Name(
            this HtmlHelper htmlHelper, string name)
        {
            return MvcHtmlString.Create(htmlHelper.AttributeEncode(htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(name)));
        }

        public static MvcHtmlString DisplayNameFor<TModel, TProperty>(
            this HtmlHelper<IEnumerable<TModel>> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return DisplayNameFor(expression);
        }

        public static MvcHtmlString DisplayNameFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return DisplayNameFor(expression);
        }

        private static MvcHtmlString DisplayNameFor<TModel, TProperty>(
            Expression<Func<TModel, TProperty>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression<TModel, TProperty>(expression, new ViewDataDictionary<TModel>());
            var htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string s = metadata.DisplayName ?? (metadata.PropertyName ?? htmlFieldName.Split(new char[] { '.' }).Last<string>());

            return new MvcHtmlString(HttpUtility.HtmlEncode(s));
        }

        public static MvcHtmlString DisplayEnumFor<TModel>(
            this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, int>> expression, Type enumType)
        {
            var value = (int)ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            string enumValue = Enum.GetName(enumType, value);

            return new MvcHtmlString(htmlHelper.Encode(enumValue));
        }
    }
}
