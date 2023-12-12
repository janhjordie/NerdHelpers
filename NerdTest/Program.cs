using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdFactory;
using NerdFactory.Helpers;
using NerdTest.Dto;
namespace NerdTest;

internal class Program
{
	private static void Main(String[] args)
	{

		IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true, true)
			.AddEnvironmentVariables()
			.Build();

		var zipKey = configuration["zipKey"] ?? "12345";

		var serviceProvider = new ServiceCollection().BuildServiceProvider();


		//	var nerdCsvService = serviceProvider.GetService<NerdCsvService>();

		var csvFileBytes = NerdFileHelpers.ReadFileToByteArray("crm-welcome.csv");
		if (csvFileBytes != null)
		{
			var csvRecords = NerdCsvHelpers.LoadCsvBytes<CrmCsvWelcomeContact>(csvFileBytes);

			var zipped = NerdZipHelpers.Zip(csvFileBytes, zipKey);

			NerdFileHelpers.SaveByteArrayToFile(zipped, "crm-welcome.csv.zip");
			var unzipped = NerdZipHelpers.Unzip(zipped, zipKey);
			var csvRecordsFromZip = NerdCsvHelpers.LoadCsvBytes<CrmCsvWelcomeContact>(unzipped);

			var unzippedCsvFileBytes = NerdZipHelpers.Unzip(NerdFileHelpers.ReadFileToByteArray("crm-welcome.csv.zip"), zipKey);

			var csvRecordsFromZip2 = NerdCsvHelpers.LoadCsvBytes<CrmCsvWelcomeContact>(unzippedCsvFileBytes);
			NerdFileHelpers.SaveByteArrayToFile(unzippedCsvFileBytes, "crm-welcome-from-zip.csv");
		}

	}
}
