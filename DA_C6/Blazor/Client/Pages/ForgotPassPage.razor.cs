//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Mail;
//using System.Net.Mime;
//using System.Threading.Tasks;
//using MimeKit;
//using MailKit.Net.Smtp;
//using Microsoft.Extensions.Configuration;
//using System.Security.Cryptography;
//using System.Text;
//using System.Net.Http.Json;
//using Blazor.Shared.Model;
//using static System.Net.WebRequestMethods;
//using Microsoft.JSInterop;
//using System.Net.Http;
//using System.Text.Json;
//using System.Text.RegularExpressions;

//namespace Blazor.Client.Pages
//{
//    public partial class ForgotPassPage
//    {
//        private List<Account> accounts;
//        private string Email;
//        private string messageSuccess = null;
//        private string messageError = null;
//        private string errorValid;
//        private bool loading = false;
//        protected override async Task OnInitializedAsync()
//        {
//            try
//            {
//                accounts = await http.GetFromJsonAsync<List<Account>>("api/Account/Getall");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//            }
//        }

//        private async Task SubmitForm()
//        {
//            try
//            {
//                var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
//                var acc = accounts.SingleOrDefault(a => a.Email == Email);
//                if (string.IsNullOrEmpty(Email))
//                {
//                    errorValid = "Vui lòng nhập Email";
//                }
//                else if (!emailRegex.IsMatch(Email))
//                {
//                    errorValid = "Email không hợp lệ";
//                }
//                else if (acc != null)
//                {
//                    loading = true;
//                    var newPassword = GenerateRandomPassword();
//                    var emailMessage = $"Mật khẩu mới là: {newPassword}";
//                    await SendEmailAsync(Email, "[NEXTON] Mật khẩu mới", emailMessage);

//                    acc.Password = GetHash(newPassword);
//                    var response = await http.PutAsJsonAsync($"api/Account/update/{acc.UserName}", acc);

//                    if (response.IsSuccessStatusCode)
//                    {
//                        messageSuccess = "Mật khẩu mới đã được gửi đến Email";
//                        messageError = null;
//                        errorValid = null;
//                        loading = false;
//                    }
//                }
//                else
//                {
//                    loading = false;
//                    errorValid = null;
//                    messageSuccess = null;
//                    messageError = "Email không tồn tại";
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex}");
//            }
//        }

//        private async Task SendEmailAsync(string email, string subject, string message)
//        {
//            var emailRequest = new EmailRequest
//            {
//                To = email,
//                Subject = subject,
//                Body = message
//            };

//            var response = await http.PostAsJsonAsync("api/Email/SendEmail", emailRequest);
//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Lỗi gửi Email");
//            }
//        }

//        private class EmailRequest
//        {
//            public string To { get; set; }
//            public string Subject { get; set; }
//            public string Body { get; set; }
//        }

//        private string GenerateRandomPassword()
//        {
//            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//            var random = new Random();
//            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
//        }

//        private string GetHash(string input)
//        {
//            using (SHA256 sha256 = SHA256.Create())
//            {
//                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
//                byte[] hashBytes = sha256.ComputeHash(inputBytes);
//                StringBuilder sb = new StringBuilder();
//                for (int i = 0; i < hashBytes.Length && i < 16; i++)
//                {
//                    sb.Append(hashBytes[i].ToString("x2"));
//                }
//                return sb.ToString();
//            }
//        }
//    }
//}
