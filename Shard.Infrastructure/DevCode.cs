using Newtonsoft.Json;

namespace Shard.Infrastructure
{
    public static class DevCode
    {
        public static string SerializeObject(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
