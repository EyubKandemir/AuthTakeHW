using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using BCrypt.Net;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:7154/api/User/GetUserWithUsername?" +
                $"username={model.Username}");


            //username = eyyub
            //pass = "123456";
           
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                dynamic userList = JsonConvert.DeserializeObject(content);

                if (userList.success == true)
                {
                    string hashPassword = userList.data.password;
                    if (BCrypt.Net.BCrypt.EnhancedVerify(model.Password,hashPassword) == true)
                    {
                        return RedirectToAction("Index","Home",new { page = 1 , username = model.Username});
                    }

                }
            }

            ModelState.AddModelError("ErrorMessage", "Kullanıcı Adı Veya Şifre Hatalı!");
            return View("Index", model);
        }
    }
}
