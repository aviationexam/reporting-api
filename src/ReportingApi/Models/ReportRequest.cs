using ReportingApi.JsonConverters;
using System.Text.Json.Serialization;

namespace ReportingApi.Models;

[JsonConverter(typeof(ReportRequestConverter))]
public class ReportRequest
{
    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("user_agent")]
    public string UserAgent { get; set; } = null!;
}

public class ReportRequest<TBody> : ReportRequest
    where TBody : class, IReportBody
{
    [JsonPropertyName("body")]
    public TBody Body { get; set; } = null!;

    [JsonPropertyName(ReportRequestConverter.TypeDiscriminator)]
    public string Type { get; set; } = null!;
}
