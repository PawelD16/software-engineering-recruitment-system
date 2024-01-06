using Microsoft.AspNetCore.Http;
using System;

namespace projektowaniaOprogramowania.Services
{
	public static class SessionExtensions
	{
		public static void SetLong(this ISession session, string key, long value)
		{
			var byteArray = BitConverter.GetBytes(value);
			session.Set(key, byteArray);
		}

		public static long? GetLong(this ISession session, string key)
		{
			var data = session.Get(key);
			if (data == null)
			{
				return null;
			}
			return BitConverter.ToInt64(data, 0);
		}
	}

}
