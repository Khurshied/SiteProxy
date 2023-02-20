using ProxyClient.Implementation;
using ProxyClient.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class SiteProxyTests
{
    private readonly ISiteProxy _siteProxy;

    public SiteProxyTests()
    {
        var httpclient = new HttpClient();

        _siteProxy = new SiteProxy("https://localhost:7031");

      

    }

    [Fact]
    public void GetPageAsync_ReturnsContentWithModifiedWords()
    {
        // Arrange
        var url = "https://habr.com";

        // Act
        var content = _siteProxy.GetModifiedContent(url);

        // Assert
        Assert.Contains("Divide and Conquer» for OpenStreetMap world inside PostgreSQL", content); // Original word     
        Assert.Contains("Divide™ and Conquer™» for OpenStreetMap world inside™ PostgreSQL", content); // Modified word
    }


}
