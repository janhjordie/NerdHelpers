using Newtonsoft.Json;
namespace NerdFactory.Converters;

public class ListToStringJoinConverter : JsonConverter<List<String>>
{
	public override void WriteJson(JsonWriter writer, List<String>? value, JsonSerializer serializer)
	{
		if (value != null) writer.WriteValue(string.Join(";", value));
	}

	public override List<String> ReadJson(JsonReader reader, Type objectType, List<String>? existingValue, Boolean hasExistingValue, JsonSerializer serializer)
	{
		return reader
			.Value?.ToString()
			?.Split(";")
			.ToList() ?? new List<String>();
	}
}
