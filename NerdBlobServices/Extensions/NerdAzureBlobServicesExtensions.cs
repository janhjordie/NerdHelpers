using Microsoft.Extensions.DependencyInjection;
using NerdFactory.Services;
namespace NerdFactory.Extensions;

public static class NerdAzureBlobServicesExtensions
{
	public static IServiceCollection AddNerdAzureBlobServices(this IServiceCollection collection)
	{
		collection.AddSingleton<NerdAzureBlobService>();

		return collection;
	}
}
