using System.Text;
namespace NerdFactory.Helpers;

public abstract class NerdFileHelpers
{
	private const String DataFolder = "Data";

	public static MemoryStream? ReadFileToMemoryStream(String filePath)
	{
		var fileWithPath = Path.Combine(DataFolder, filePath);

		if (!File.Exists(fileWithPath)) return null;

		using var fileStream = new FileStream(fileWithPath, FileMode.Open, FileAccess.Read);
		var memoryStream = new MemoryStream();
		fileStream.CopyTo(memoryStream);

		return memoryStream;

	}

	public static Byte[]? ReadFileToByteArray(String filePath)
	{
		var fileWithPath = Path.Combine(DataFolder, filePath);

		if (!File.Exists(fileWithPath)) return null;

		return File.ReadAllBytes(fileWithPath);
	}

	public static String? ReadFileToString(String filePath, Encoding? encoding = null)
	{
		var fileWithPath = Path.Combine(DataFolder, filePath);

		if (!File.Exists(fileWithPath)) return null;

		encoding = encoding ?? Encoding.UTF8;
		using var reader = new StreamReader(fileWithPath, encoding);

		return reader.ReadToEnd();

	}

	public static void SaveMemoryStreamToFile(MemoryStream memoryStream, String filePath)
	{
		var fileWithPath = Path.Combine(DataFolder, filePath);

		using var fileStream = new FileStream(fileWithPath, FileMode.Create, FileAccess.Write);
		memoryStream.WriteTo(fileStream);
	}

	public static void SaveByteArrayToFile(Byte[] byteArray, String filePath)
	{
		var fileWithPath = Path.Combine(DataFolder, filePath);

		File.WriteAllBytes(fileWithPath, byteArray);
	}

	public static void SaveStringToFile(String content, String filePath, Encoding? encoding = null)
	{
		var fileWithPath = Path.Combine(DataFolder, filePath);

		encoding = encoding ?? Encoding.UTF8;

		File.WriteAllText(fileWithPath, content, encoding);
	}
}
