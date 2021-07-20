using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IRedisCacheService
    {
        public T Get<T>(string key);
        public T Set<T>(string key, T value);
        public void Update(string key, string newKey);
        public Task<T> GetAsync<T>(string key);
        public Task<T> SetAsync<T>(string key, T value);
    }
}
