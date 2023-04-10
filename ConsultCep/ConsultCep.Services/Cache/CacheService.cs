using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultCep.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributed;
        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;

        public CacheService(IDistributedCache distributed)
        {
            _distributed = distributed;
            _distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                  AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2),
                   SlidingExpiration = TimeSpan.FromMinutes(1)
            };
        }
        public async Task<string> GetAsync(string key)
        {
            return await _distributed.GetStringAsync(key);
        }

        public async Task SetAsync(string key, string value)
        {
             await _distributed.SetStringAsync(key, value, _distributedCacheEntryOptions);
        }
    }
}
