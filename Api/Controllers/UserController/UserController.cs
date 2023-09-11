using Business.Managers;
using Data.Tables;
using Data.Results;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager _userManager;

        public UserController(ILogger<UserController> logger, UserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet]
        public DataResult<User> GetUser()
        {
            return _userManager.GetUser();
        }

        [HttpGet]
        public DataResult<User> GetUserWithId(int id)
        {
            return _userManager.GetUserById(id);
        }

        [HttpGet]
        public DataResult<User> GetUserWithEmail(string email)
        {
            return _userManager.GetUserByEmail(email);
        }

        [HttpGet]
        public DataResult<User> GetUserWithUsername(string username)
        {
            return _userManager.GetUserByUserName(username);
        }

        [HttpGet]
        public DataResult<List<User>> GetUsers()
        {
            return _userManager.GetUserList();
        }

        [HttpPost]
        public Result AddUser(User user)
        {
            return _userManager.AddUser(user);
        }

        [HttpPut]
        public Result UpdateUser(User user)
        {
            return _userManager.UpdateUser(user);
        }
    }
}
