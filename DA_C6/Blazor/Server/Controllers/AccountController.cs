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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Blazor.Server.Data;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount account;
        private readonly ApplicationDbContext _context;
        public AccountController(IAccount acc, ApplicationDbContext context)
        {
            _context = context;
            account = acc;
        }

        [HttpGet]
        [Route("GetAll")]
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
        [Route("Add")]
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
        [HttpGet("signin-google")]
        public IActionResult SignInGoogle()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            // Lấy thông tin người dùng từ principal
            var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;

            // Gọi API hoặc truy xuất dữ liệu từ cơ sở dữ liệu để lấy thông tin tài khoản người dùng
            var user = _context.Accounts.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                // Xử lý khi không tìm thấy người dùng

                var newUserName = "NguoiDung" + _context.Accounts.Count() + 1; // Tạo tên người dùng mới
                var newUser = new Account()
                {
                    UserName = newUserName,
                    Password = "25d55ad283aa400af464c76d713c07ad",  // Mật khẩu tạm thời, bạn nên tạo một mật khẩu an toàn
                    Email = email,
                    Role = "Member",
                    Name = "User",
                    Phone = " ",
                    Gender = "Nam",
                    Address = " "
                };
                _context.Accounts.Add(newUser);
                _context.SaveChanges();
                user = newUser;
            }
            return LocalRedirectPreserveMethod($"/response-google?userName={user.UserName}"); // Chuyển hướng sau khi đăng nhập thành công
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