using ProxyClient.Interfaces;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ProxyClient.Implementation
{
    public class SiteProxy : ISiteProxy
    {
        private readonly HttpClient _httpClient;

        private readonly string _proxyServerAddress;

        public SiteProxy(string proxyServerAddress)
        {
            _httpClient = new HttpClient();
            _proxyServerAddress = proxyServerAddress;
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
