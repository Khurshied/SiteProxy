using Microsoft.Extensions.Options;
using ProxyClient.Configuration;
using ProxyClient.Interfaces;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ProxyClient.Implementation
{
    public class SiteProxy : ISiteProxy
    {
        private readonly HttpClient _httpClient;
        private readonly string _proxyServerAddress;
        private readonly int _maxRetries;

        public SiteProxy(IOptions<ProxyServerOptions> options)
        {
            _httpClient = new HttpClient();
            _proxyServerAddress = options.Value.Address;
            _maxRetries = options.Value.MaxRetries;
        }

        public string GetModifiedContent(string url)
        {

            var content = _httpClient.GetStringAsync(url).Result;
            content = ModifyContent(content);

            content = ReplaceInternalLinks(content);

            return content;
        }

        private static string ModifyContent(string content) => Regex.Replace(content, @"\b\w{6}\b", "$0™");

        private string ReplaceInternalLinks(string content)
        {
            // Replace internal links with the proxy server's address
            return Regex.Replace(content, @"(href|src)=""/", $"$1=\"{_proxyServerAddress}/");
        }
    }
}
