using Data.Tables;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Web.Models;

namespace Web.Controllers
{
    public class AddNewSiteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public AddNewSiteController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> Index(int? id, string username)
        {
            AddNewSiteModel model = new AddNewSiteModel();
            model.UserName = username;
            if (id.HasValue)
            {
                model.Id = id.Value;
                var httpClient = _httpClientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:7154/api/Sites/GetSiteById?" +
                    $"id={model.Id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    dynamic userList = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    model.Password = userList.data.password;
                    model.SiteName = userList.data.siteName;

                }
            }
            return View("~/Views/Home/AddNewSiteView.cshtml", model);
        }

        public async Task<JsonResult> InsertSiteInfo(string sitename, string username, string password)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var site = new Sites
            {
                Password = password,
                SiteName = sitename,
                UserName = username
            };
            
            var json = JsonSerializer.Serialize(site);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:7154/api/Sites/AddSite", content);
            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = true, message = "Site başarıyla eklendi" });
            }

            return new JsonResult(new { success = false, message = "Veri eklerken hata oldu" });
        }

        public async Task<JsonResult> EditSiteInfo(int id, string sitename, string username, string password)
        {

            var httpClient = _httpClientFactory.CreateClient();
            var editsite = new Sites
            {
                Id = id,
                Password = password,
                SiteName = sitename,
                UserName = username
            };

            var json = JsonSerializer.Serialize(editsite);
            var site = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync("http://localhost:7154/api/Sites/UpdateSite", site);
            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(new { success = true, message = "Site başarıyla güncellendi" });
            }

            return new JsonResult(new { success = false, message = "Veri düzenlerken hata oldu" });
        }




    }
}
