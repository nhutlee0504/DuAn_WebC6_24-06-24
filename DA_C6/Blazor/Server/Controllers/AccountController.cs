using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;
using System;
using Blazor.Model;
using Account = Blazor.Shared.Model.Account;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ISessionServices _sessionServices;

        public AccountController(ISessionServices sessionServices)
        {
            _sessionServices = sessionServices;
        }

        [HttpGet("username")]
        public IActionResult GetUsername()
        {
            var username = _sessionServices.GetUsername();
            return Ok(username);
        }
        private readonly IAccount account;

        public AccountController(IAccount acc) => account = acc;

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var user = account.LoginAccount(model.UserName, model.Password);
                if (user == null)
                {
                    return Unauthorized(new { message = "Tài khoản hoặc mật khẩu không đúng" });
                }

                HttpContext.Session.SetString("LoggedInUser", user.UserName);
                Console.WriteLine($"Login successful for user: {user.UserName}");

                return Ok(new { message = "Đăng nhập thành công", role = user.Role });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet]
        public IEnumerable<Shared.Model.Account> GetAll()
        {
            var accounts = account.GetAccounts();
            Console.WriteLine("Retrieved all accounts successfully.");
            return accounts;
        }

        [HttpPost]
        public IActionResult Add([FromBody] Account acc)
        {
            if (acc == null)
                return BadRequest("Invalid account data");

            var addedAccount = account.AddAccount(new Account
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

            Console.WriteLine($"Account added successfully: {addedAccount.UserName}");

            return CreatedAtAction(nameof(GetUser), new { user = addedAccount.UserName }, addedAccount);
        }

        [HttpGet("{user}")]
        public IActionResult GetUser(string user)
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Invalid username");

            var account = this.account.GetAccountById(user);
            if (account == null)
                return NotFound();

            Console.WriteLine($"Retrieved account: {account.UserName}");
            return Ok(account);
        }

        [HttpPut("{user}")]
        public IActionResult Update(string user, [FromBody] Account acc)
        {
            if (string.IsNullOrEmpty(user) || acc == null)
                return BadRequest("Invalid username or account data");

            var updatedAccount = account.UpdateAccount(user, new Account
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

            if (updatedAccount == null)
                return NotFound();

            Console.WriteLine($"Account updated successfully: {updatedAccount.UserName}");
            return Ok(updatedAccount);
        }

        [HttpDelete("{user}")]
        public IActionResult Delete(string user)
        {
            if (string.IsNullOrEmpty(user))
                return BadRequest("Invalid username");

            account.DeleteAccount(user);
            Console.WriteLine($"Account deleted successfully: {user}");
            return NoContent();
        }
    }
}
