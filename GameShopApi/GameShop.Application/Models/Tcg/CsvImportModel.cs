using System.Text.Json.Serialization;

namespace GameShop.Application.Models.Tcg;
public class CsvImportModel
{
    public string? Name { get; set; }

    //TODO fix this withou complaing about int/string cant deserailze
    //[JsonPropertyName("Card Number")]
    //public int CardNumber { get; set; }

    [JsonPropertyName("Set Code")]
    public string? SetCode { get; set; }
}
