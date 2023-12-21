using System.ComponentModel.DataAnnotations;
namespace NerdFactory.Options;

public class NerdAzureBlobOptions
{
	public const String AppSettingKey = "NerdAzureBlob";

	[Required]
	public required String ConnectionString { get; init; }

	[Required]
	public required String ContainerName { get; init; }
}
