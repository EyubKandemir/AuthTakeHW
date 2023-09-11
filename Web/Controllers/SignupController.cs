using Data.Tables;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Web.Models;

namespace Web.Controllers
{
	public class SignupController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;

		public SignupController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SignupAsync(SignupModel model)
		{
			var httpClient = _httpClientFactory.CreateClient();

			var user = new User
			{
				UserName = model.Username,
				Password = model.Password,
				FirstName = model.Name,
				LastName = model.Surname,
				Email = model.Email
			};

			var json = JsonSerializer.Serialize(user);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync("http://localhost:7154/api/User/AddUser", content);

			if (response.IsSuccessStatusCode )
			{
				return RedirectToAction("Index", "Login");
			}

			return View("Index", model);
		}
	}
}
