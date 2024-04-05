using GameShop.Application.Converters;
using System.Text.Json.Serialization;

namespace GameShop.Application.Models.Tcg;
public class ImportModel
{
    public string? Name { get; set; }

    [JsonPropertyName("Card Number")]
    [JsonConverter(typeof(JsonIntToString))]
    public string? CardNumber { get; set; }

    [JsonPropertyName("Set Code")]
    public string? SetCode { get; set; }

    public string? Printing { get; set; }
}