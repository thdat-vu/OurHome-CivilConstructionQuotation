using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

namespace SWP391.CHCQS.Utility.Helpers
{
	public static class SessionExtensions
	{
		public static void Set<T>(this ISession session, string key, T value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}

		public static T? Get<T>(this ISession session, string key)
		{
			var value = session.GetString(key);
			return value == null ? default : JsonSerializer.Deserialize<T>(value);
		}
     

        public static void SetBoolean(this ISession session, string key, bool value)
		{
			session.SetString(key, value.ToString());
		}

		public static bool? GetBoolean(this ISession session, string key)
		{
			var value = session.GetString(key);
			if (bool.TryParse(value, out bool result))
			{
				return result;
			}
			return null;
		}
	}
}
