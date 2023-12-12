using System.Text;
namespace NerdFactory.NerdHelpers;

public abstract class NerdConvertHelper
{

	public static Stream StringToStream(String input)
	{
		var byteArray = Encoding.UTF8.GetBytes(input);

		return new MemoryStream(byteArray);
	}

	public static String StreamToString(Stream input)
	{
		using var reader = new StreamReader(input, Encoding.UTF8);

		return reader.ReadToEnd();

	}

	public static Byte[] StreamToByteArray(Stream input)
	{
		using var memoryStream = new MemoryStream();
		input.CopyTo(memoryStream);

		return memoryStream.ToArray();

	}

	public static Stream ByteArrayToStream(Byte[] input)
	{
		return new MemoryStream(input);
	}


	public static Byte[] StringToByteArray(String input)
	{
		return Encoding.UTF8.GetBytes(input);
	}

	public static String ByteArrayToString(Byte[] input)
	{
		return Encoding.UTF8.GetString(input);
	}

	public static MemoryStream StringToMemoryStream(String input)
	{
		var byteArray = Encoding.UTF8.GetBytes(input);

		return new MemoryStream(byteArray);
	}

	public static String MemoryStreamToString(MemoryStream input)
	{
		using var reader = new StreamReader(input, Encoding.UTF8);

		return reader.ReadToEnd();

	}

	public static Byte[] MemoryStreamToByteArray(MemoryStream input)
	{
		return input.ToArray();
	}

	public static MemoryStream ByteArrayToMemoryStream(Byte[] byteArray)
	{
		return new MemoryStream(byteArray);
	}
}
