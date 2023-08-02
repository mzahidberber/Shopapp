using Microsoft.AspNetCore.Mvc;
using shopapp.core.CrossCuttingConcers.Caching;
using shopapp.core.Reflection;

namespace shopapp.web.Controllers
{
    public class BaseController:Controller
    {
        public ICacheManager _cacheManager { get; set; }
        public BaseController(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }
        protected IActionResult? CheckCache(Type type,string methodName,params object[]? parametres)
        {
            var key = Reflection.CreateCacheKey(type, methodName, parametres);
            if (_cacheManager.IsAdd(key))
                return _cacheManager.Get<IActionResult>(key);
            return null;
        }
    }
}
