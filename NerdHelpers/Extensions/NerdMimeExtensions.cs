using System.Text.Json.Serialization;
namespace NerdFactory.Extensions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentFileType
{
	Unknown,
	PDF,
	PNG,
	CSV,
	TSV,
	PARQUET,
	JPG,
	DOCX,
	XLSX,
	GIF
}
public static class NerdMimeExtensions
{
	public static String? GetMimeTypeForFileExtension(String filePath)
	{
		const String defaultContentType = "application/octet-stream";

		if (MimeTypes.TryGetMimeType(filePath, out var contentType))
		{
			contentType = defaultContentType;
		}

		return contentType;
	}

	public static DocumentFileType GetDocumentFileTypeForFileExtension(String filePath)
	{
		switch (Path
			        .GetExtension(filePath)
			        .ToLower())
		{
			case ".doc":
			case ".docx":
				return DocumentFileType.DOCX;
			case ".xsl":
			case ".xlsx":
				return DocumentFileType.XLSX;
			case ".png": return DocumentFileType.PNG;
			case ".pdf": return DocumentFileType.PDF;
			case ".jpg":
			case ".jpeg":
				return DocumentFileType.JPG;
			case ".gif": return DocumentFileType.GIF;
			case ".csv": return DocumentFileType.CSV;
			case ".tsv": return DocumentFileType.TSV;
			case ".parquet": return DocumentFileType.PARQUET;
			default: return DocumentFileType.Unknown;
		}
	}
}
