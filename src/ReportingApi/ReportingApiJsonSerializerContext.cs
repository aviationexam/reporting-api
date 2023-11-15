using ReportingApi.Models;
using System.Text.Json.Serialization;

namespace ReportingApi;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    GenerationMode = JsonSourceGenerationMode.Default
)]
[JsonSerializable(typeof(ReportRequest))]
[JsonSerializable(typeof(ReportRequest<CspReport>))]
[JsonSerializable(typeof(ReportRequest<NetworkErrorReport>))]
public sealed partial class ReportingApiJsonSerializerContext : JsonSerializerContext;
