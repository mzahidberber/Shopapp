using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace shopapp.core.Aspects.Logging
{
    public class LogAspectController : ActionFilterAttribute
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var message = string.Format("{0} | {1} | {2} | {3}",
                filterContext.Controller.GetType().Namespace,
                filterContext.Controller.GetType().Name,
                filterContext.ActionDescriptor.DisplayName,
                GetParams(filterContext)
            );
            _logger.Info(message);
        }

        private string GetParams(ActionExecutingContext IInvocation)
        {
            var parametres = "";

            foreach (var argument in IInvocation.ActionArguments)
            {
                parametres += $"{argument.Key}:{argument.Value} | ";

            }
            return parametres;
        }

    }
}
