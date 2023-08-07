using Castle.DynamicProxy;
using shopapp.core.CrossCuttingConcers.Caching;

namespace shopapp.core.Aspects.Caching
{
    public class CacheAspect : Aspect
    {
        private Type _cacheType;
        private int _cacheByMinute;
        private ICacheManager _cacheManager;
        public CacheAspect(Type cacheType, int cacheByMinute = 60)
        {
            _cacheType = cacheType;
            _cacheByMinute = cacheByMinute;
        }
        public override void OnBefore(IInvocation invocation)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            var methodName = invocation.Method.Name;
            var arguments = invocation.Arguments;

            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));

            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get<object>(key);
                Console.WriteLine(invocation.ReturnValue);
            }

        }

        public override void OnAfter(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;
            var arguments = invocation.Arguments;

            var key = string.Format("{0}({1})", methodName,
                string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>")));

            if (_cacheManager.IsAdd(key))
                invocation.ReturnValue = _cacheManager.Get<object>(key);



            _cacheManager.Add(key, invocation.ReturnValue, _cacheByMinute);

        }
    }
}
