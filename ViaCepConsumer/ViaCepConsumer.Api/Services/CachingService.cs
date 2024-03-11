using Microsoft.Extensions.Caching.Distributed;
using ViaCepConsumer.Api.Services.Interfaces;

namespace ViaCepConsumer.Api.Services
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };
        }

        public async Task Set(string key, string value)
        {
            await _cache.SetStringAsync(key, value, _options);
        }

        public async Task<string> Get(string key)
        {
            return await _cache.GetStringAsync(key);
        }
    }
}
