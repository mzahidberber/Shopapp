using NLog;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace shopapp.core.CrossCuttingConcers.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache => MemoryCache.Default;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public void Add(string key, object data, int cacheTime)
        {
            _logger.Info($"{key} data:{data} cache add!");
            if (data == null)
                return;

            var policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime) };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public void Clear()
        {
            _logger.Info("All cache clear!");
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }

        public T Get<T>(string key)
        {
            _logger.Info($"{key} Get Cache!");
            return (T)Cache[key];
        }

        public bool IsAdd(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            _logger.Info($"{key} remove cache!");
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            _logger.Info($"{pattern} remove pattern cache!");
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = Cache.Where(d => regex.IsMatch(d.Key)).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }
    }
}
