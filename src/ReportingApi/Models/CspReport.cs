using System.Text.Json.Serialization;

namespace ReportingApi.Models;

public sealed record CspReport : IReportBody
{
    [JsonPropertyName("blockedURL")]
    public string BlockedUri { get; set; } = null!;

    [JsonPropertyName("disposition")]
    public string Disposition { get; set; } = null!;

    [JsonPropertyName("documentURL")]
    public string DocumentUri { get; set; } = null!;

    [JsonPropertyName("effectiveDirective")]
    public string EffectiveDirective { get; set; } = null!;

    [JsonPropertyName("lineNumber")]
    public int LineNumber { get; set; }

    [JsonPropertyName("originalPolicy")]
    public string OriginalPolicy { get; set; } = null!;

    [JsonPropertyName("referrer")]
    public string Referrer { get; set; } = null!;

    [JsonPropertyName("sample")]
    public string Sample { get; set; } = null!;

    [JsonPropertyName("sourceFile")]
    public string SourceFile { get; set; } = null!;

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
}
