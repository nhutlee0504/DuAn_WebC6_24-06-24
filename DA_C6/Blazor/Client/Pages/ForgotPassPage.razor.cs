using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Net.Http.Json;
using Blazor.Shared.Model;
using static System.Net.WebRequestMethods;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Text.Json;

namespace Blazor.Client.Pages
{
    public partial class ForgotPassPage
    {
        private List<Account> accounts;
        private Account account = new Account();
        private string messageSuccess = null;
        private string messageError = null;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                accounts = await http.GetFromJsonAsync<List<Account>>("api/account/getall");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private async Task SubmitForm()
        {
            try
            {
                var acc = accounts.SingleOrDefault(a => a.Email == account.Email);
                if (string.IsNullOrEmpty(account.Email)) { }
                else if (acc != null)
                {
                    var newPassword = GenerateRandomPassword();
                    var emailMessage = $"Mật khẩu mới là: {newPassword}";
                    await SendEmailAsync(account.Email, "Mật khẩu mới", emailMessage);

                    acc.Password = GetHash(account.Password);
                    var response = await http.PatchAsync($"api/Account/{acc.UserName}", new StringContent(JsonSerializer.Serialize(acc), Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        messageSuccess = "Mật khẩu mới đã gửi đến Email của bạn";
                        messageError = null;
                    }
                    else
                    {
                        messageSuccess = null;
                        messageError = "Không thể cập nhật mật khẩu. Vui lòng thử lại sau.";
                    }
                }
                else
                {
                    messageSuccess = null;
                    messageError = "Email không tồn tại";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        private async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailRequest = new EmailRequest
            {
                To = email,
                Subject = subject,
                Body = message
            };

            var response = await http.PostAsJsonAsync("api/Email/SendEmail", emailRequest);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Lỗi gửi Email");
            }
        }

        private class EmailRequest
        {
            public string To { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
        }

        private string GenerateRandomPassword()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GetHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length && i < 16; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
