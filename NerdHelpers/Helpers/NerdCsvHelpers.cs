using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace NerdFactory.Helpers;

public abstract class NerdCsvHelpers
{
	private const String DataFolder = "Data";

	public static IEnumerable<T> LoadCsvMemoryStream<T>(MemoryStream? csvStream, String delimiter = ";")
	{
		if (csvStream == null)
			return [];

		csvStream.Position = 0;
		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			Delimiter = delimiter
		};

		using var reader = new StreamReader(csvStream);
		using var csv = new CsvReader(reader, config);
		csv.Read();
		csv.ReadHeader();

		return csv
			.GetRecords<T>()
			.ToList();

	}

	public static IEnumerable<T> LoadCsvBytes<T>(Byte[]? csvBytes, String delimiter = ";")
	{
		if (csvBytes == null) return [];

		var stream = NerdConvertHelper.ByteArrayToMemoryStream(csvBytes);
		var records = LoadCsvMemoryStream<T>(stream, delimiter);
		stream.Dispose();

		return records;
	}

	public static IEnumerable<T> LoadCsvString<T>(String csvString, String delimiter = ";")
	{
		if (string.IsNullOrWhiteSpace(csvString)) return new List<T>();

		var stream = NerdConvertHelper.StringToMemoryStream(csvString);
		var records = LoadCsvMemoryStream<T>(NerdConvertHelper.StringToMemoryStream(csvString), delimiter);
		stream.Dispose();

		return records;
	}

	public static IEnumerable<T> LoadCsvFile<T>(String csvfile, String delimiter = ";")
	{
		if (string.IsNullOrWhiteSpace(csvfile)) return new List<T>();

		using var reader = new StreamReader(csvfile);
		using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		{
			var records = csv.GetRecords<T>();

			return records;
		}
	}

	public static void ToCsvFile<T>(IEnumerable<T> contacts, String csvFileName)
	{
		var fileWithPath = Path.Combine(DataFolder, csvFileName);

		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = true,
			Delimiter = ";"
		};

		using var writer = new StreamWriter(fileWithPath);

		using var csv = new CsvWriter(writer, config);
		csv.WriteRecords(contacts);
		csv.Flush();
		writer.Flush();



	}


	public static String ToCsvString<T>(IEnumerable<T> records, String delimiter = ";")
	{
		using var writer = new StringWriter();
		using var csv = new CsvWriter(writer, new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture)
		{
			Delimiter = delimiter
		});

		csv.WriteRecords(records);

		return writer.ToString();


	}

	public static MemoryStream ToCsvStream<T>(IEnumerable<T> contacts)
	{
		var str = new MemoryStream();
		var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		{
			HasHeaderRecord = true,
			Delimiter = ";"
		};

		using (var stream = new MemoryStream())
		using (var writer = new StreamWriter(stream))
		using (var csv = new CsvWriter(writer, config))
		{
			csv.WriteRecords(contacts);
			csv.Flush();
			writer.Flush();
			stream.Position = 0;
			stream.CopyTo(str);
		}

		str.Position = 0;

		return str;
	}

	public static Byte[] ToCsvByteArray<T>(IEnumerable<T> contacts)
	{
		var stream = ToCsvStream(contacts);
		stream.Position = 0;
		var bytes = NerdConvertHelper.MemoryStreamToByteArray(stream);
		stream.Dispose();

		return bytes;
	}
}
