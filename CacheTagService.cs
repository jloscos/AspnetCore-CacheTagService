using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.AspNetCore.Mvc.TagHelpers.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheTagService
{
    public interface ICacheTagService
    {
        IList<ReadableCacheTagKey> GetKeys();
        void Remove(ReadableCacheTagKey key);
        void RemoveWhere(Func<ReadableCacheTagKey, bool> predicate);
    }

    public class CacheTagService : ICacheTagService
    {
        private IMemoryCache memoryCache;

        public CacheTagService(CacheTagHelperMemoryCacheFactory factory)
        {
            memoryCache = factory.Cache;
        }

        public IList<ReadableCacheTagKey> GetKeys()
        {
            return memoryCache.GetCacheKeys<CacheTagKey>().Select(k => new ReadableCacheTagKey(k)).ToList();
        }

        public void Remove(ReadableCacheTagKey key)
        {
            memoryCache.Remove(key.CacheTagKey);
        }

        public void RemoveWhere(Func<ReadableCacheTagKey, bool> predicate)
        {
            foreach (var k in GetKeys().Where(predicate))
                Remove(k);
        }
    }
}
