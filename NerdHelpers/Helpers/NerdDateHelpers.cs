using System.Globalization;
namespace NerdFactory.Helpers;

public static class NerdDateHelpers
{
	public static DateTime UnixTimeStampToDateTime(Int64 unixTimeStamp)
	{
		return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp)
			.DateTime;
	}

	public static DateTime ToTimeZone(this DateTime utcDate, String timeZone = "Romance Standard Time")
	{
		var timeZoneDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDate, timeZone);

		return timeZoneDateTime;
	}

	public static String ToDanish(this DateTime utcDate, String format = "dd-MM-yyyy HH:mm")
	{
		var timeZoneDateTime = utcDate.ToTimeZone();

		return timeZoneDateTime.ToString(format, CultureInfo.InvariantCulture);
	}
}
