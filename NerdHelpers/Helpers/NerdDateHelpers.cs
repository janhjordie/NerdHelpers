namespace NerdFactory.Helpers;

public static class NerdDateHelpers
{
	public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
	{
		return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).DateTime;
	}
}
