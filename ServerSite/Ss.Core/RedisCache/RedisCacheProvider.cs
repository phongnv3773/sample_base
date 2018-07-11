
using ServiceStack.Redis;
using StackExchange.Redis;
using System;


namespace Ss.Core.RedisCache
{
    public class RedisCacheProvider : ICacheProvider
    {
        RedisEndpoint _endPoint;
        string host = "localhost";
        string port = "6379";
        string password = null;

        public RedisCacheProvider(string connect)
        {
            if (!string.IsNullOrEmpty(connect))
            {
                var hostAndPort = connect.Split(':');
                host = hostAndPort[0];
                port = hostAndPort[1];
            }

            _endPoint = new RedisEndpoint(host, Convert.ToInt32(port));
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, TimeSpan.Zero);
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (RedisClient client = new RedisClient(_endPoint))
            {
                client.As<T>().SetValue(key, value, timeout);
            }
        }

        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient client = new RedisClient(_endPoint))
            {
                var wrapper = client.As<T>();

                result = wrapper.GetValue(key);
            }

            return result;
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }


        public bool FlushAllDatabases()
        {
            try
            {
                using (ConnectionMultiplexer redisConnect = ConnectionMultiplexer.Connect(string.Format("{0},{1}", string.Format("{0}:{1}", host, port), "allowAdmin=true")))
                {
                    var server = redisConnect.GetServer(string.Format("{0}:{1}", host, port));
                    server.FlushAllDatabases();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
