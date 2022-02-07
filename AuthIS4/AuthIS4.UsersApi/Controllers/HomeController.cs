using IdentityModel.Client;
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
            var authClient = clientFactory.CreateClient();
            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001");
            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "OrdersApi"
            });

            var clientOrders = clientFactory.CreateClient();
            clientOrders.SetBearerToken(tokenResponse.AccessToken);
            var response = await clientOrders.GetAsync("https://localhost:7001/Home/GetSecret");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.SecretMessage = response.StatusCode.ToString();
                return View();
            }
            var response_secret_test = await response.Content.ReadAsStringAsync();
            ViewBag.SecretMessage = response_secret_test;
            return View();
        }
    }
}
