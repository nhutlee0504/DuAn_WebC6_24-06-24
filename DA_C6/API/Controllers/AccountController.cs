using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Model;
using API.Services;
using System.Collections.Generic;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount account;
        public AccountController(IAccount acc) => account = acc;
        [HttpGet("details/{userName}")]
        public ActionResult<Account> GetAccountDetails(string userName)
        {
            var account1 = account.GetAccountById(userName);
            if (account1 == null)
            {
                return NotFound();
            }

            return account1;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Account model)
        {
            var user = account.LoginAccount(model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không đúng" });
            }

            HttpContext.Session.SetString("LoggedInUser", user.UserName);

            return Ok(new { message = "Đăng nhập thành công", role = user.Role });
        }
        [HttpGet]
        public IEnumerable<Account> GetAll()
        {
            return account.GetAccounts();
        }

        [HttpPost]
        public Account Add(Account acc)
        {
            return account.AddAccount(new Account
            {
                UserName = acc.UserName,
                Password = acc.Password,
                Email = acc.Email,
                Role = acc.Role,
                Address = acc.Address,
                Phone = acc.Phone,
                Gender = acc.Gender,
                Name = acc.Name
            });
        }

        [HttpGet("{user}")]
        public Account GetUser(string user)
        {
            if (string.IsNullOrEmpty(user))
                return null;
            return account.GetAccountById(user);
        }

        [HttpPut("{user}")]
        public Account Update(Account acc, string user)
        {
            if (string.IsNullOrEmpty(user))
                return null;
            return account.UpdateAccount(user, acc);
        }

        [HttpDelete("{user}")]
        public IActionResult Delete(string user)
        {
            if (string.IsNullOrEmpty(user))
                return null;
            account.DeleteAccount(user);
            return NoContent();
        }
    }
}