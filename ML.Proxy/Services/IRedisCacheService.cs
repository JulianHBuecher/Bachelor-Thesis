namespace ML.Proxy.Services
{
    public interface IRedisCacheService
    {
        public T Get<T>(string key);
        public T Set<T>(string key, T value);
    }
}
