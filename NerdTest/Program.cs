using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdFactory;
using NerdFactory.Extensions;
using NerdFactory.Helpers;
using NerdFactory.Services;
using NerdTest.Dto;
namespace NerdTest;

internal class Program
{
	private async static Task Main(String[] args)
	{

		IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", true, true)
			.AddEnvironmentVariables()
			.Build();

		var serviceProvider = new ServiceCollection()
			.BuildServiceProvider();
		
			var nerdAzureBlob = new NerdAzureBlobService(configuration);
			var files = await nerdAzureBlob.FilesAsync("crmtool");
		

		void TestCsv()
		{
			var zipKey = configuration["zipKey"] ?? "12345";




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
}
