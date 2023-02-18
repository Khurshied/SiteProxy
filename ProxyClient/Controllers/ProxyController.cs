using Microsoft.AspNetCore.Mvc;
using ProxyClient.Interfaces;
using ProxyClient.Models;
using System.Diagnostics;

namespace ProxyClient.Controllers
{
    public class ProxyController : Controller
    {
        private readonly ILogger<ProxyController> _logger;
        private readonly ISiteProxy _siteProxy;
        public ProxyController(ILogger<ProxyController> logger,ISiteProxy siteProxy )
        {
            _logger = logger;
            _siteProxy = siteProxy;
        }

        public IActionResult Index()
        {
            var content = _siteProxy.GetModifiedContent("https://habr.com");

            return Content(content, "text/html");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}