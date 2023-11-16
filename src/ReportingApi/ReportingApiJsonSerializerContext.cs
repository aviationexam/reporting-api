using ReportingApi.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReportingApi;

[JsonSourceGenerationOptions(
    GenerationMode = JsonSourceGenerationMode.Default
)]
[JsonSerializable(typeof(ReportRequest))]
[JsonSerializable(typeof(ReportRequest<CspReport>))]
[JsonSerializable(typeof(ReportRequest<NetworkErrorReport>))]
[JsonSerializable(typeof(IReadOnlyCollection<ReportRequest>))]
public sealed partial class ReportingApiJsonSerializerContext : JsonSerializerContext;
