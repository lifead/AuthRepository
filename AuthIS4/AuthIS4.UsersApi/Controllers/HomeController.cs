using Microsoft.AspNetCore.Mvc;

namespace AuthIS4.UsersApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrderSecretAsync()
        {
            var clientOrders = clientFactory.CreateClient();
            var response_secret_test = await clientOrders.GetStringAsync("https://localhost:7001/Home/GetSecret");
            ViewBag.SecretMessage = response_secret_test;
            return View();
        }
    }
}
