using System.Web.Mvc;
using BookingsExample.Domain;

namespace BookingsExample
{
    internal static class RulesViolationExceptionExtensions
    {
        public static void CopyTo(this RulesException ex, ModelStateDictionary modelState)
        {
            CopyTo(ex, modelState, null);
        }

        public static void CopyTo(this RulesException ex, ModelStateDictionary modelState, string prefix)
        {
            prefix = string.IsNullOrEmpty(prefix) ? "" : prefix + ".";
            foreach (var propertyError in ex.Errors) {
                string key = ExpressionHelper.GetExpressionText(propertyError.Property);
                modelState.AddModelError(prefix + key, propertyError.Message);
            }
        }
    }
}