using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Blazor.Shared.Model;
using Blazor.Server.Services;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Routing;

namespace Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccount account;
        public AccountController(IAccount acc) => account = acc;

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<Account> GetAll()
        {
            return account.GetAccounts();
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

        [HttpGet("{user}")]
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

        // Trong file AccountController.cs
        [HttpPost("Authenticate")]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] Account model)
        {
            // Lấy salt từ cơ sở dữ liệu hoặc sử dụng một giá trị cố định
            string salt = "somesalt";

            // Kết hợp mật khẩu người dùng với salt
            string combinedPassword = string.Concat(model.Password, salt);

            // Băm mật khẩu kết hợp
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Băm mật khẩu kết hợp
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));

                // Chuyển byte[] thành string hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length && i < 16; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Giá trị băm
                string hashedPassword = builder.ToString();

                // Giả sử account là một đối tượng của lớp AccountService chứa phương thức VerifyPassword
                var authenticated = account.VerifyPassword(model.UserName, hashedPassword);

                if (authenticated)
                {
                    return Ok(new { message = "Authentication successful" });
                }

                return BadRequest(new { message = "Authentication failed" });
            }
        }
    }
}
