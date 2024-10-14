using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Customer.API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validate Model State on each request and return a Bad request if invalid
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Verify the Model state is valid for each request
            if (!context.ModelState.IsValid)
            {
                // Respond with Bad request
                context.Result = new BadRequestResult();
            }
        }
    }
}
