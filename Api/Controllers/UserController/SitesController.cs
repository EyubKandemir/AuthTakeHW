using Business.Managers;
using Data.Results;
using Data.Tables;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SitesController
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager _userManager;

        public SitesController(ILogger<UserController> logger, UserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public DataResult<List<Sites>> GetSitesByUsername(string username)
        {
            return _userManager.GetSitesByUsername(username);
        }

        [HttpGet]
        public DataResult<Sites> GetSiteById(int id)
        {
            return _userManager.GetSiteById(id);
        }

        [HttpPut]
        public Result UpdateSite(Sites site)
        {
            return _userManager.UpdateSite(site);
        }

        [HttpPost]
        public Result AddSite(Sites site)
        {
            return _userManager.AddSite(site);
        }

        [HttpDelete]
        public Result DeleteSite(int id)
        {
            return _userManager.DeleteSite(id);
        }
    }
}
