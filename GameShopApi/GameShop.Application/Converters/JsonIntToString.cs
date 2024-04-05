using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameShop.Application.Converters;
public class JsonIntToString : JsonConverter<string>
{
    public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => 
        reader.TokenType != JsonTokenType.String && reader.TryGetInt32(out var value) ? 
            value.ToString() : reader.GetString();

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) => throw new NotImplementedException();
}
