using Microsoft.AspNetCore.Mvc.Filters;
using shopapp.core.CrossCuttingConcers.Caching;

namespace shopapp.core.Aspects.Caching
{
    public class CacheAspectController: ActionFilterAttribute
    {
        private Type _cacheType;
        private int _cacheByMinute;
        private ICacheManager _cacheManager;
        private List<string> _arguments;
        public CacheAspectController(Type cacheType, int cacheByMinute = 60):base()
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
            _arguments = new List<string>();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            _arguments.Clear();
            var methodName = context.ActionDescriptor.RouteValues["action"];
            var parametres = context.Controller.GetType().GetMethod(methodName).GetParameters();
            if (parametres != null)
            {
                foreach (var p in parametres)
                {
                    if (context.ActionArguments.ContainsKey(p.Name))
                        _arguments.Add(context.ActionArguments[p.Name].ToString());
                    else
                        _arguments.Add(p.DefaultValue?.ToString() ?? "<Null>");
                }
            }

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var methodName = string.Format("{0} | {1} | {2}",
                context.Controller.GetType().Namespace,
                context.Controller.GetType().Name,
                context.ActionDescriptor.RouteValues["action"]
            );

            var key = string.Format("{0}({1})",methodName,string.Join(",",_arguments));
            
            if (!_cacheManager.IsAdd(key))
                _cacheManager.Add(key, context.Result, _cacheByMinute);
        }

    }
}
