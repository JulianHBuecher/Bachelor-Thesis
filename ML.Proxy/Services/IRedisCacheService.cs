using System.Threading.Tasks;

namespace ML.Proxy.Services
{
    public interface IRedisCacheService
    {
        public T Get<T>(string key);
        public void Set<T>(string key, T value);
        public void UpdateLabel(string key, string newKey);
        public void Remove(string key);
        public Task<T> GetAsync<T>(string key);
        public Task SetAsync<T>(string key, T value);
        public Task UpdateLabelAsync(string key, string newKey);
        public Task RemoveAsync(string key);
    }
}
