using Business.Messages;
using Business.PasswordHash;
using Business.ValidationRules.Site;
using Business.ValidationRules.User;
using Data.Contexts;
using Data.Results;
using Data.Tables;
using System.Security.Cryptography;
using System.Text;

namespace Business.Managers
{
	public class UserManager
	{
		private readonly DatabaseContext _context;
		private const string HashKey = "dfbn12as123okdavSFA";
		public UserManager(DatabaseContext context)
		{
			_context = context;
		}

		#region User

		public DataResult<User> GetUser()
		{
			User? user = _context.Users.FirstOrDefault();

			if (user == null)
				return new ErrorDataResult<User>(UserMessages.UserNotFound);

			return new SuccessDataResult<User>(user);
		}

		public DataResult<User> GetUserById(int id)
		{
			User? user = _context.Users.FirstOrDefault(x => x.Id == id);

			if (user == null)
				return new ErrorDataResult<User>(UserMessages.UserNotFound);

			return new SuccessDataResult<User>(user);
		}

		public DataResult<User> GetUserByEmail(string email)
		{
			User? user = _context.Users.FirstOrDefault(x => x.Email == email);

			if (user == null)
				return new ErrorDataResult<User>(UserMessages.UserNotFound);

			return new SuccessDataResult<User>(user);
		}

		public DataResult<User> GetUserByUserName(string username)
		{
			User? user = _context.Users.FirstOrDefault(x => x.UserName == username);

			if (user == null)
				return new ErrorDataResult<User>(UserMessages.UserNotFound);

			return new SuccessDataResult<User>(user);
		}

		public DataResult<List<User>> GetUserList()
		{
			List<User> userList = _context.Users.ToList();

			if (userList.Count == 0)
				return new ErrorDataResult<List<User>>(UserMessages.UserNotFound);

			return new SuccessDataResult<List<User>>(userList);
		}

		public Result AddUser(User user)
		{
			Result result = new UserAddValidator().Run(user);
			if (!result.Success)
				return result;

			if (!UserAdded(user))
				return new Result(false, UserMessages.UserAlreadyExists);
			user.Password = HashUserPass(user.Password);
			_context.Users.Add(user);
			_context.SaveChanges();

			return new SuccessResult();
		}

		private string? HashUserPass(string? password)
		{
			string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);
			return passwordHash;
		}

		private bool UserAdded(User user)
		{
			User? hasUser = _context.Users.FirstOrDefault(x => x.UserName == user.UserName);
			if (hasUser == null) return true;
			return false;
		}

		public Result UpdateUser(User user)
		{
			Result result = new UserUpdateValidator().Run(user);
			if (!result.Success)
				return result;

			if (!UserAdded(user))
				return new Result(false, UserMessages.UserAlreadyExists);

			_context.Users.Update(user);
			_context.SaveChanges();

			return new SuccessResult();
		}

		#endregion

		#region Sites


		public DataResult<List<Sites>> GetSitesByUsername(string username)
		{
			List<Sites> sites = _context.Sites.Where(x => x.UserName == username).ToList();
			if (sites.Count == 0)
				return new ErrorDataResult<List<Sites>>(SitesMessages.SitesNotFound);

			foreach (Sites site in sites)
			{
				site.Password = PasswordEncryptDecrypt.Decrypt(site.Password, "EyyubEyyubEyyubE");
			}
			return new SuccessDataResult<List<Sites>>(sites);
		}

		public DataResult<Sites> GetSiteById(int id)
		{
			Sites? site = _context.Sites.FirstOrDefault(x => x.Id == id);

			if (site == null)
				return new ErrorDataResult<Sites>(SitesMessages.SiteNotFound);

			site.Password = PasswordEncryptDecrypt.Decrypt(site.Password, "EyyubEyyubEyyubE");

			return new SuccessDataResult<Sites>(site);
		}

		public Result UpdateSite(Sites site)
		{
			Result result = new SiteUpdateValidator().Run(site);
			if (!result.Success)
				return result;

			site.Password = PasswordEncryptDecrypt.Encrypt(site.Password, "EyyubEyyubEyyubE");

			_context.Sites.Update(site);
			_context.SaveChanges();

			return new SuccessResult();
		}

		public Result AddSite(Sites site)
		{
			Result result = new SiteAddValidator().Run(site);
			if (!result.Success)
				return result;
			site.Password = PasswordEncryptDecrypt.Encrypt(site.Password, "EyyubEyyubEyyubE");
			_context.Sites.Add(site);
			_context.SaveChanges();
			return new SuccessResult();
		}

		public Result DeleteSite(int id)
		{
			if (id == 0) return new ErrorResult(SitesMessages.IdCantBeZeroOrNull);

			Sites? site = _context.Sites.FirstOrDefault(x => x.Id == id);
			if (site == null)
				return new ErrorDataResult<Sites>(SitesMessages.SiteNotFound);

			_context.Sites.Remove(site);
			_context.SaveChanges();
			return new SuccessResult();

		}

		#endregion

	}

}
