using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdFactory.Options;
using NerdFactory.Services;
namespace NerdFactory.Extensions;

public static class NerdAzureBlobServicesExtensions
{
	public static IServiceCollection AddNerdAzureBlobServices(this IServiceCollection collection, IConfiguration configuration)
	{
		collection
			.AddOptions<NerdAzureBlobOptions>()
			.BindConfiguration(NerdAzureBlobOptions.AppSettingKey)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		collection.Configure<NerdAzureBlobOptions>(configuration.GetSection(NerdAzureBlobOptions.AppSettingKey));

		collection.AddSingleton<NerdAzureBlobService>();

		return collection;
	}
}
