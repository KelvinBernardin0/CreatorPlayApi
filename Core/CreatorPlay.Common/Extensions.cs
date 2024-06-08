using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CreatorPlay.Common;

public static class Extensions
{
	public static string GetDescription(this Enum value)
	{
		var field = value.GetType().GetField(value.ToString());
		var attributes = field?.GetCustomAttributes(typeof(DescriptionAttribute), false);
		return attributes?.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString() ?? value.ToString();
	}

	public static bool HasValue(this string value)
	{
		return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
	}

	public static string ToJson(this object obj, bool igonoreNull = false)
	{
		if (igonoreNull)
			return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Local });

		return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Local });
	}

	public static string ToIndentedJson(this object obj, bool igonoreNull = false)
	{
		if (igonoreNull)
			return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Local });

		return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Local });
	}

	public static T FromJson<T>(this string value)
	{
		return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
	}

	public static bool TryParseJson<T>(this string value, out T result)
	{
		try
		{
			result = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
			return true;
		}
		catch
		{
			result = default(T);
			return false;
		}
	}

	public static Guid ToGuid(this string value)
	{
		return new Guid(value);
	}

	public static long ToUnixTimestamp(this DateTime dateTime)
	{
		return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local)).TotalSeconds;
	}

	public static string UnMask(this string value)
	{
		return value.HasValue() ? value.Replace("-", "").Replace(".", "").Replace("/", "") : value;
	}

	public static string GetDisplayText(this Enum value)
	{
		string itemDescription;
		var attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayTextAttribute), false);
		if (attributes.Length == 0)
		{
			attributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
			itemDescription = attributes.Length != 0 ? ((DescriptionAttribute)attributes[0]).Description : value.ToString();
		}
		else
			itemDescription = ((DisplayTextAttribute)attributes[0]).DisplayText;

		return itemDescription;
	}

	public static IServiceCollection RegisterOptions<T>(this IServiceCollection services, string configurationPath = null) where T : class
	{
		services.AddOptions<T>().Configure(cfg => Configuration.GetConfiguration().Bind(configurationPath ?? typeof(T).Name, cfg));
		return services;
	}

	public static string ToQueryString(this object obj)
	{
		var properties = from p in obj.GetType().GetProperties()
						 where p.GetValue(obj, null) != null
						 select p.Name + "=" + System.Web.HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

		var queryString = string.Join("&", properties.ToArray());
		return queryString;
	}

	public static bool IsValidEmail(this string email)
	{
		if (email == null)
			return false;

		Regex regex = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
		Match match = regex.Match(email);

		return match.Success;
	}

	public static T[] GetEnumValues<T>() where T : struct
	{
		if (!typeof(T).IsEnum)
			throw new ArgumentException("Ocorreu um erro interno.");

		return (T[])Enum.GetValues(typeof(T));
	}
}
