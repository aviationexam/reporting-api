using System.Text.Json.Serialization;

namespace ReportingApi.Models;

public sealed record NetworkErrorReport : IReportBody
{
    [JsonPropertyName("sampling_fraction")]
    public decimal SamplingFraction { get; set; }

    [JsonPropertyName("referrer")]
    public string Referrer { get; set; } = null!;

    [JsonPropertyName("server_ip")]
    public string ServerIp { get; set; } = null!;

    [JsonPropertyName("protocol")]
    public string Protocol { get; set; } = null!;

    [JsonPropertyName("method")]
    public string Method { get; set; } = null!;

    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("elapsed_time")]
    public int ElapsedTime { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;
}
