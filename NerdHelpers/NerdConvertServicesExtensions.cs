using Microsoft.Extensions.DependencyInjection;
using NerdConvert.Services;
namespace NerdConvert;

public static class NerdConvertServicesExtensions
{
	public static IServiceCollection AddNerdConvertServices(this IServiceCollection collection)
	{
		collection.AddSingleton<NerdCsvService>();

		return collection;
	}
}
