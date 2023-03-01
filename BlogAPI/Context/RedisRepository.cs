using StackExchange.Redis;

namespace BlogAPI.Context;

public class RedisRepository
{
    private static Lazy<ConnectionMultiplexer>
        lazyConnection = new Lazy <ConnectionMultiplexer>(() =>
        {
            string redisCacheConnection = "";//_config["RedisCacheSecretKey"];
            return ConnectionMultiplexer.Connect(redisCacheConnection);
        });

    public static ConnectionMultiplexer Connection
    {
        get
        {
            return lazyConnection.Value;
        }
    }
}