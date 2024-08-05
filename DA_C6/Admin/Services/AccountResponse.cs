using Admin.Data;
using Admin.Model;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Admin.Services
{
	public class AccountResponse : IAccount
	{
		private readonly ApplicationDbContext context;
		public AccountResponse(ApplicationDbContext ct)
		{
			context = ct;
		}

		public Account AddAccount(Account account)
		{
			try
			{
				context.Accounts.Add(account);
				context.SaveChanges();
				return account;
			}
			catch (System.Exception)
			{

				return null;
			}
		}

		public bool Authenticate(string username, string password)
		{
			// Tìm tài khoản trong cơ sở dữ liệu dựa trên username
			var user = context.Accounts.FirstOrDefault(x => x.UserName == username);

			if (user != null && VerifyPasswordHash(password, user.Password))
			{
				return true; // Mật khẩu đúng
			}

			return false; // Mật khẩu sai hoặc tên tài khoản không tồn tại
		}

		public void DeleteAccount(string user)
		{
			var us = context.Accounts.Find(user);
			if (us != null)
			{
				context.Accounts.Remove(us);
				context.SaveChanges();
			}
		}

		public Account GetAccountById(string user)
		{
			return context.Accounts.Find(user);
		}

		public IEnumerable<Account> GetAccounts()
		{
			return context.Accounts;
		}

		public Account UpdateAccount(string user, Account account)
		{
			try
			{
				var us = context.Accounts.Find(user);
				if (us != null)
				{
					us.Password = account.Password;
					us.Email = account.Email;
					us.Role = account.Role;
					us.Name = account.Name;
					us.Gender = account.Gender;
					us.Phone = account.Phone;
					us.Address = account.Address;
					context.SaveChanges();
					return account;
				}
				return null;
			}
			catch (System.Exception)
			{

				return null;
			}
		}

		public bool VerifyPassword(string username, string password)
		{
			// Tìm tài khoản trong cơ sở dữ liệu dựa trên username
			var user = context.Accounts.FirstOrDefault(x => x.UserName == username);

			if (user != null)
			{
				// So sánh mật khẩu đã băm với mật khẩu người dùng nhập vào
				if (VerifyPasswordHash(password, user.Password))
				{
					return true; // Mật khẩu đúng
				}
			}

			return false; // Mật khẩu sai hoặc tên tài khoản không tồn tại
		}

		private bool VerifyPasswordHash(string password, string storedHash)
		{
			// Thực hiện quá trình băm mật khẩu nhập vào và so sánh với giá trị đã lưu
			// Đây là nơi bạn cần triển khai quá trình băm mật khẩu và so sánh chuỗi băm với giá trị đã lưu trong cơ sở dữ liệu.
			// Bạn có thể sử dụng cùng một thuật toán băm và salt như phần tạo mật khẩu (trong phương thức AddAccount)

			// Một ví dụ sử dụng SHA256 để băm mật khẩu và so sánh với giá trị đã lưu
			using (SHA256 sha256Hash = SHA256.Create())
			{
				string hashedPassword = GetHash(sha256Hash, password);
				return storedHash.Equals(hashedPassword);
			}
		}

		private string GetHash(HashAlgorithm hashAlgorithm, string input)
		{
			// Chuyển đổi input thành mảng byte và thực hiện băm
			byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Chuyển byte[] thành string hex
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
			{
				builder.Append(data[i].ToString("x2"));
			}

			return builder.ToString();
		}


	}
}
