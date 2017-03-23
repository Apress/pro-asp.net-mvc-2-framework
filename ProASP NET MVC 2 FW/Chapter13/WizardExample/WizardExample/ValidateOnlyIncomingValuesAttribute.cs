using System;
using System.Linq;
using System.Web.Mvc;

namespace WizardExample
{
public class ValidateOnlyIncomingValuesAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var modelState = filterContext.Controller.ViewData.ModelState;
        var incomingValues = filterContext.Controller.ValueProvider;

        var keys = modelState.Keys.Where(x => !incomingValues.ContainsPrefix(x));
        foreach (var key in keys) // These keys don't match any incoming value
            modelState[key].Errors.Clear();
    }
}
}