using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
namespace NerdBlob.Services;

public class NerdAzureBlobService
{
	private readonly BlobContainerClient _blobContainer;

	public NerdAzureBlobService(IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("AzureBlobStorage");
		var containerName = configuration.GetConnectionString("AzureBlobStorageContainerName");

		var blobServiceClient = new BlobServiceClient(connectionString);
		_blobContainer = blobServiceClient.GetBlobContainerClient(containerName);
	}

	public async Task UploadAsync(Byte[] data, String destinationPath)
	{
		var blobClient = _blobContainer.GetBlobClient(destinationPath);
		using var stream = new MemoryStream(data);
		await blobClient.UploadAsync(stream, true);
	}

	public async Task<Byte[]> DownloadAsync(String sourcePath)
	{
		var blobClient = _blobContainer.GetBlobClient(sourcePath);

		using var stream = new MemoryStream();
		var response = await blobClient.DownloadAsync();
		await response.Value.Content.CopyToAsync(stream);

		return stream.ToArray();

	}
}
