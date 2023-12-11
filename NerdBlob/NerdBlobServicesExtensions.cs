using Microsoft.Extensions.DependencyInjection;
using NerdBlob.Services;
namespace NerdBlob;

public static class NerdBlobServicesExtensions
{
	public static IServiceCollection AddNerdBlobServices(this IServiceCollection collection)
	{
		collection.AddSingleton<NerdAzureBlobService>();
		return collection;
	}
}
