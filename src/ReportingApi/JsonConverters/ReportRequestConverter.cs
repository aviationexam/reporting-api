using ReportingApi.Models;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReportingApi.JsonConverters;

public class ReportRequestConverter : JsonConverter<ReportRequest>
{
    public const string TypeDiscriminator = "type";

    public override ReportRequest? Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options
    )
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        var readerClone = reader;

        if (readerClone.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var depth = readerClone.CurrentDepth + 1;
        while (readerClone.Read())
        {
            if (depth < readerClone.CurrentDepth)
            {
                continue;
            }

            switch (readerClone.TokenType)
            {
                case JsonTokenType.PropertyName:
                    if (readerClone.GetString() == TypeDiscriminator)
                    {
                        readerClone.Read();

                        if (readerClone.TokenType is JsonTokenType.String)
                        {
                            var bodyType = GetBodyType(readerClone.ValueSpan);

                            var targetType = typeof(ReportRequest<>).MakeGenericType(bodyType);

                            return (ReportRequest?) JsonSerializer.Deserialize(
                                ref reader, targetType, options
                            );
                        }
                    }

                    break;

                case JsonTokenType.StartArray:
                case JsonTokenType.StartObject:
                    readerClone.Skip();

                    break;
            }
        }

        throw new JsonException($"Expected '{TypeDiscriminator}' property, nothing found");
    }

    private Type GetBodyType(ReadOnlySpan<byte> type)
    {
        if (type.SequenceEqual("csp-violation"u8))
        {
            return typeof(CspReport);
        }

        if (type.SequenceEqual("network-error"u8))
        {
            return typeof(NetworkErrorReport);
        }

        var typeString = Encoding.UTF8.GetString(type);
        throw new JsonException($"Type '{typeString}' does not have defined body type");
    }

    public override void Write(
        Utf8JsonWriter writer, ReportRequest value, JsonSerializerOptions options
    ) => throw new NotImplementedException();
}
