using Persistence.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Core.Services
{
    public class RedisService<T> where T : class
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task<T> GetDataAsync(string key, Todo getDataFromDb)
        //public async Task<T?> GetDataAsync(string key, Func<T?> getDataFromDb)
        {
            var db = _redis.GetDatabase();
            var cachedData = await db.StringGetAsync(key);

            if (!cachedData.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }

            //var dataFromDb = await getDataFromDb();
            Todo dataFromDb = getDataFromDb;

            if (dataFromDb != null)
            {
                var serializedData = JsonSerializer.Serialize(dataFromDb);
                await db.StringSetAsync(key, serializedData, TimeSpan.FromMinutes(10));
            }

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        // Tambahin fungsi untuk post data/ create data ke redis
        public async Task CreateDataAsync(string key, Todo getDataFromDb)
        {
            var db = _redis.GetDatabase();
            var serializedData = JsonSerializer.Serialize(getDataFromDb);
            await db.StringSetAsync(key, serializedData, TimeSpan.FromMinutes(10));
        }

    }
}