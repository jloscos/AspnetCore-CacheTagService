using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CacheTagService
{
    public static class CacheTagServiceExtensions
    {
        public static List<T> GetCacheKeys<T>(this IMemoryCache memoryCache)
        {
            if (memoryCache.GetType() != typeof(MemoryCache))
                throw new NotSupportedException("Argument muse be of type MemoryCache");
            var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
            var collection = field.GetValue(memoryCache) as ICollection;
            var items = new List<T>();
            if (collection != null)
                foreach (var item in collection)
                {
                    var methodInfo = item.GetType().GetProperty("Key");
                    var val = methodInfo.GetValue(item);
                    if (val is T k)
                        items.Add(k);
                }
            return items;
        }

        public static void AddCacheTagService(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<ICacheTagService, CacheTagService>();
        }
    }
}
