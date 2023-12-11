using ICSharpCode.SharpZipLib.Zip;
namespace NerdConvert.NerdHelpers;

public abstract class NerdZipHelpers
{
	public static void Zip(MemoryStream input, MemoryStream output, String? zipKey)
	{
		using var zipStream = new ZipOutputStream(output);
		if (!string.IsNullOrEmpty(zipKey))
		{
			zipStream.Password = zipKey;
		}

		var entry = new ZipEntry("data");
		zipStream.PutNextEntry(entry);

		input.CopyTo(zipStream);
		zipStream.CloseEntry();
	}

	public static Byte[]? Zip(Byte[] inputData, String? zipKey)
	{
		using var input = new MemoryStream(inputData);
		using var output = new MemoryStream();
		Zip(input, output, zipKey);

		return output.ToArray();
	}

	public static void Unzip(MemoryStream input, MemoryStream output, String? zipKey)
	{

		using var zipStream = new ZipInputStream(input);
		if (!string.IsNullOrEmpty(zipKey))
		{
			zipStream.Password = zipKey;
		}

		while (zipStream.GetNextEntry() is {} entry)
		{
			if (entry.Name != "data") continue;

			zipStream.CopyTo(output);

			break;
		}
	}

	public static Byte[] Unzip(Byte[] zipped, String? zipKey)
	{
		using var input = new MemoryStream(zipped);
		using var output = new MemoryStream();
		Unzip(input, output, zipKey);

		return output.ToArray();

	}
}
