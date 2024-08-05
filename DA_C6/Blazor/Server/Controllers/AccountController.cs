using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;
using System.Text;
using System.Security.Cryptography;
using System;
using static Blazor.Model.Account;
using Blazor.Model;
using Account = Blazor.Shared.Model.Account;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount account;
        public AccountController(IAccount acc) => account = acc;

        [HttpGet]
        public IEnumerable<Account> GetAll()
        {
            return account.GetAccounts();
        }
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
        [Route("GetUser/{user}")]
        public Account GetUser(string user)
        {
            if (string.IsNullOrEmpty(user))
                return null;
            return account.GetAccountById(user);
        }

        [HttpPut("{user}")]
        [Route("Update/{user}")]
        public Account Update(string user, Account acc)
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] Shared.Model.LoginModel model)
        {
            try
            {
                var user = account.LoginAccount(model.UserName, model.Password);
                if (user == null)
                {
                    return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không đúng" });
                }

                HttpContext.Session.SetString("LoggedInUser", user.UserName);
                HttpContext.Session.SetString("UserRole", user.Role); 
                Console.WriteLine($"Login successful for user: {user.UserName}");

                return Ok(new { message = "Đăng nhập thành công", role = user.Role });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


    }
}
