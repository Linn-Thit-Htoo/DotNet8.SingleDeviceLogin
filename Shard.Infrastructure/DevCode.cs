using Newtonsoft.Json;

namespace Shard.Infrastructure
{
	public static class DevCode
	{
		public static string SerializeObject(this object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		public static T DeserializeObject<T>(this string jsonStr)
		{
			return JsonConvert.DeserializeObject<T>(jsonStr)!;
		}

		public static string GetCurrentMyanmarDateTime()
		{
			var myanmarTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
			var myanmarDateTime = TimeZoneInfo.ConvertTime(
				DateTime.Now,
				TimeZoneInfo.Local,
				myanmarTimeZone
			);

			return myanmarDateTime.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}
