using ReportingApi.Models;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace ReportingApi.Tests;

public class ReportRequestConverterTests
{
    [Theory]
    [MemberData(nameof(JsonSerializerOptionsData))]
    public void CspViolationTest(JsonSerializerOptions jsonSerializerOptions)
    {
        const string sourceJson =
            // language=json
            """
            {
              "age":1,
              "body": {
                "blockedURL":"https://csplite.com/tst/media/7_del.png",
                "disposition":"enforce",
                "documentURL":"https://csplite.com/tst/test_frame.php?ID=229&hash=da964209653e467d337313e51876e27d",
                "effectiveDirective":"img-src",
                "lineNumber":9,
                "originalPolicy":"default-src 'none'; report-to endpoint-csp;",
                "referrer":"https://csplite.com/test229/",
                "sourceFile":"https://csplite.com/tst/test_frame.php?ID=229&hash=da964209653e467d337313e51876e27d",
                "statusCode":0
              },
              "type":"csp-violation",
              "url":"https://csplite.com/tst/test_frame.php?ID=229&hash=da964209653e467d337313e51876e27d",
              "user_agent":"Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36"
            }
            """;

        var reportRequest = JsonSerializer.Deserialize<ReportRequest>(sourceJson, jsonSerializerOptions);

        Assert.NotNull(reportRequest);
        Assert.Equal(1, reportRequest.Age);
        Assert.Equal(
            "https://csplite.com/tst/test_frame.php?ID=229&hash=da964209653e467d337313e51876e27d",
            reportRequest.Url
        );
        Assert.Equal(
            "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36",
            reportRequest.UserAgent
        );

        var cspReport = Assert.IsType<ReportRequest<CspReport>>(reportRequest);
        Assert.Equal("csp-violation", cspReport.Type);
        Assert.NotNull(cspReport.Body);
    }

    [Theory]
    [MemberData(nameof(JsonSerializerOptionsData))]
    public void NetworkErrorTest(JsonSerializerOptions jsonSerializerOptions)
    {
        const string sourceJson =
            // language=json
            """
            {
              "age": 2,
              "type": "network-error",
              "url": "https://widget.com/thing.js",
              "body": {
                "sampling_fraction": 1.0,
                "referrer": "https://www.example.com/",
                "server_ip": "",
                "protocol": "",
                "method": "GET",
                "status_code": 0,
                "elapsed_time": 143,
                "type": "dns.name_not_resolved"
              }
            }
            """;

        var reportRequest = JsonSerializer.Deserialize<ReportRequest>(sourceJson, jsonSerializerOptions);

        Assert.NotNull(reportRequest);
        Assert.Equal(2, reportRequest.Age);
        Assert.Equal("https://widget.com/thing.js", reportRequest.Url);
        Assert.Null(reportRequest.UserAgent);

        var cspReport = Assert.IsType<ReportRequest<NetworkErrorReport>>(reportRequest);
        Assert.Equal("network-error", cspReport.Type);
        Assert.NotNull(cspReport.Body);
    }

    public static IEnumerable<object?[]> JsonSerializerOptionsData()
    {
        yield return new object?[]
        {
            null,
        };
        yield return new object?[]
        {
            ReportingApiJsonSerializerContext.Default.Options,
        };
    }
}
