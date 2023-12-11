using CsvHelper.Configuration.Attributes;
namespace NerdTest.Dto;

[Delimiter(";")]
public class CrmCsvWelcomeContact
{
	// FastOpfordring
	// Ændret
	// Oprettet
	// Startdato
	// Hubspotid
	// Medlemsnummer
	// Navn
	// Email
	// Email2
	// Medlemstype
	// Kanal
	// EmneOpretÅrsag
	// PostnrBy
	// Land
	// Sats
	// BetalingsBeløb
	// BetalingsFrekvens
	// Afdeling
	// Ophørt
	// OphørÅrsag
	// FravalgtNM
	// Firmanavn
	// Attention;



	[Index(2)]
	[Optional]
	public DateTime? Modified { get; set; }

	[Index(3)]
	[Optional]
	public DateTime? Oprettet { get; set; }

	[Index(4)]
	public DateTime? Created { get; set; }

	[Index(5)]
	[Optional]
	public String? HubspotId { get; set; }

	[Index(6)]
	public String? Medlemsnr { get; set; }

	[Index(7)]
	[Optional]
	public String? Name { get; set; }

	[Index(8)]
	[Optional]
	public String? Email { get; set; }

	[Index(9)]
	[Optional]
	public String? Email2 { get; set; }

	[Index(10)]
	[Optional]
	public String? Medlemstype { get; set; }

	[Index(11)]
	[Optional]
	public String? Kanal { get; set; }

	[Index(12)]
	[Optional]
	public String? EmneOpretÅrsag { get; set; }

	[Index(13)]
	[Optional]
	public String? PostnrBy { get; set; }

	[Index(14)]
	[Optional]
	public String? Land { get; set; }

	[Index(15)]
	[Optional]
	public Int32? Sats { get; set; }

	[Index(16)]
	[Optional]
	public Decimal? BetalingsBeløb { get; set; }

	[Index(17)]
	[Optional]
	public String? BetalingsFrekvens { get; set; }

	[Index(18)]
	[Optional]
	public String? Afdeling { get; set; }

	[Index(19)]
	[Optional]
	public String? Ophørt { get; set; }

	[Index(20)]
	[Optional]
	public String? OphørÅrsag { get; set; }

	[Index(21)]
	[Optional]
	public String? FravalgtNM { get; set; }


	[Index(22)]
	[Optional]
	public String? Firmanavn { get; set; }


	[Index(23)]
	[Name("Attention")]
	[Optional]
	public String? Attention { get; set; }
}
