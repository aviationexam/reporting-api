using ReportingApi.Models;
using System.Text.Json;
using Xunit;

namespace ReportingApi.Tests;

public class ReportRequestConverterTests
{
    [Fact]
    public void CspViolationTest()
    {
        var sourceJson = """
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

        var reportRequest = JsonSerializer.Deserialize<ReportRequest>(sourceJson);

        Assert.NotNull(reportRequest);
        Assert.Equal(1, reportRequest.Age);
        Assert.Equal("https://csplite.com/tst/test_frame.php?ID=229&hash=da964209653e467d337313e51876e27d", reportRequest.Url);
        Assert.Equal("Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36", reportRequest.UserAgent);

        var cspReport = Assert.IsType<ReportRequest<CspReport>>(reportRequest);
        Assert.Equal("csp-violation", cspReport.Type);
        Assert.NotNull(cspReport.Body);
    }
}
