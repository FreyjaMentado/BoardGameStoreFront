using System.Text.Json.Serialization;

namespace GameShop.Application.Models.Tcg;
public class CsvImportModel
{
    public string? Name { get; set; }

    [JsonPropertyName("Set Code")]
    public string? SetCode { get; set; }
}
