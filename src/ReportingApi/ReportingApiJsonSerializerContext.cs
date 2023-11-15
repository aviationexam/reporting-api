using ReportingApi.Models;
using System.Text.Json.Serialization;

namespace ReportingApi;

[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Default
)]
[JsonSerializable(typeof(ReportRequest))]
[JsonSerializable(typeof(ReportRequest<CspReport>))]
[JsonSerializable(typeof(ReportRequest<NetworkErrorReport>))]
public sealed partial class ReportingApiJsonSerializerContext : JsonSerializerContext;
