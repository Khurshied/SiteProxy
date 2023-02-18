using ProxyService.Interface;
using System.Text.RegularExpressions;

namespace ProxyService.Implementation
{
    public class SiteProxy: ISiteProxy
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

        private string ModifyContent(string content)
        {
            return Regex.Replace(content, @"\b\w{6}\b", "$0™");
        }

        private string ReplaceInternalLinks(string content)
        {
            // Replace internal links with the proxy server's address
            return Regex.Replace(content, @"(href|src)=""/", $"$1=\"{_proxyServerAddress}/");
        }
    }
}
