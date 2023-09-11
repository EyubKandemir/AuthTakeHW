using Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web.Models;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IHttpClientFactory _httpClientFactory;


		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
		{
			_logger = logger;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index(string username, int page = 1)
		{
			var httpClient = _httpClientFactory.CreateClient();
			HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:7154/api/Sites/GetSitesByUsername?" +
				$"username={username}");

			HomeModel model = new HomeModel();	
			model.Username = username;
			List<Sites> sites = new List<Sites>();
			int pageSize = 10; // Her sayfada görüntülenecek site sayısı
			List<Sites> paginatedSites = new List<Sites>();
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				dynamic userList = JsonConvert.DeserializeObject(content);
				dynamic list = userList.data;

				if (list != null)
				{
                    foreach (dynamic site in list)
                    {
                        sites.Add(new Sites()
                        {
                            Id = site.id,
                            Password = site.password,
                            SiteName = site.siteName,
                            UserName = site.userName
                        });
                    }
                    paginatedSites = sites.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                }
			}
			model.Sites = paginatedSites;
			return View(model);

		}

		public async Task<JsonResult> Delete(int id)
		{
			var httpClient = _httpClientFactory.CreateClient();
			HttpResponseMessage response = await httpClient.DeleteAsync($"http://localhost:7154/api/Sites/DeleteSite?" +
				$"id={id}");
			if (response.IsSuccessStatusCode)
			{
				return new JsonResult(new { success = true, message = "Silme işlemi başarılı" });
			}

			return new JsonResult(new { success = false, message = "Silme işlemi başarısız" }); 
		}

       
        //refresh
        public async Task<ActionResult> GetGrid(string username)
		{
			int page = 1;
			var httpClient = _httpClientFactory.CreateClient();
			HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:7154/api/Sites/GetSitesByUsername?" +
				$"username={username}");
			List<Sites> sites = new List<Sites>();
			int pageSize = 10; // Her sayfada görüntülenecek site sayısı

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				dynamic userList = JsonConvert.DeserializeObject(content);
				dynamic list = userList.data;

				foreach (dynamic site in list)
				{
					sites.Add(new Sites()
					{
						Id = site.id,
						Password = site.password,
						SiteName = site.siteName,
						UserName = site.userName
					});
				}
			}
			var paginatedSites = sites.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			return View(paginatedSites);
		}
	}
}