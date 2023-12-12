using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using NerdFactory.Helpers;
namespace NerdFactory.Services;

public class NerdAzureBlobService
{
	private readonly BlobContainerClient _blobContainer;
	private readonly BlobServiceClient _blobServiceClient;

	public NerdAzureBlobService(IConfiguration configuration)
	{
		if (string.IsNullOrWhiteSpace(configuration["AzureBlobStorage"]))
			throw new Exception("AzureBlobStorage setting is missing");

		var connectionString = configuration["AzureBlobStorage"];
		var containerName = configuration["AzureBlobStorageContainerName"] ?? "blobs";

		_blobServiceClient = new BlobServiceClient(connectionString);
		_blobContainer = CreateOrGetBlobContainer(containerName);
	}

	private BlobContainerClient CreateOrGetBlobContainer(String container)
	{
		container = container.ToLower();
		var containers = _blobServiceClient
			.GetBlobContainers()
			.ToList();

		if (!containers.Any(x => x.Name.Equals(container)))
			_blobServiceClient.CreateBlobContainer(container);

		var blobContainer = _blobServiceClient.GetBlobContainerClient(container);

		return blobContainer;
	}


	public async Task UploadAsync(Byte[] data, String destinationPath)
	{
		var blobClient = _blobContainer.GetBlobClient(destinationPath);
		using var stream = new MemoryStream(data);
		await blobClient.UploadAsync(stream, true);
	}

	public async Task UploadAsync(Byte[] data, String container, String destinationPath)
	{
		var blobContainer = CreateOrGetBlobContainer(container);
		var blobClient = blobContainer.GetBlobClient(destinationPath);
		using var stream = new MemoryStream(data);
		await blobClient.UploadAsync(stream, true);
	}


	public async Task CompressAndUploadAsync(Byte[] data, String destinationPath, String key)
	{
		var compressed = NerdZipHelpers.Zip(data, key);
		if (compressed != null) await UploadAsync(compressed, destinationPath);
	}

	public async Task CompressAndUploadAsync(Byte[] data, String container, String destinationPath, String key)
	{
		var compressed = NerdZipHelpers.Zip(data, key);
		if (compressed != null) await UploadAsync(compressed, container.ToLower(), destinationPath);
	}


	public async Task<Byte[]> DownloadAsync(String sourcePath)
	{
		var blobClient = _blobContainer.GetBlobClient(sourcePath);

		using var stream = new MemoryStream();
		var response = await blobClient.DownloadAsync();
		await response.Value.Content.CopyToAsync(stream);

		return stream.ToArray();

	}

	public async Task<Byte[]> DownloadAndDecompressAsync(String sourcePath, String key)
	{
		var compressed = await DownloadAsync(sourcePath);
		var decompressed = NerdZipHelpers.Unzip(compressed, key);

		return decompressed;
	}

	public async Task<Byte[]> DownloadAsync(String container, String sourcePath)
	{
		var blobContainer = CreateOrGetBlobContainer(container);
		var blobClient = blobContainer.GetBlobClient(sourcePath);

		using var stream = new MemoryStream();
		var response = await blobClient.DownloadAsync();
		await response.Value.Content.CopyToAsync(stream);

		return stream.ToArray();

	}

	public async Task<Byte[]> DownloadAndDecompressAsync(String container, String sourcePath, String key)
	{
		var compressed = await DownloadAsync(container, sourcePath);
		var decompressed = NerdZipHelpers.Unzip(compressed, key);

		return decompressed;
	}

	public async Task<List<BlobItem>> FilesAsync()
	{
		var blobs = new List<BlobItem>();
		await foreach (var blob in _blobContainer.GetBlobsAsync())
		{
			blobs.Add(blob);
		}

		return blobs;
	}

	public async Task<List<BlobItem>> FilesAsync(String container)
	{
		var blobContainer = CreateOrGetBlobContainer(container);
		var blobs = new List<BlobItem>();
		await foreach (var blob in blobContainer.GetBlobsAsync())
		{
			blobs.Add(blob);
		}

		return blobs;
	}
}
