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

		public Account LoginAccount(string username, string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(password);
				byte[] hashBytes = sha256.ComputeHash(inputBytes);
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length && i < 16; i++)
				{
					sb.Append(hashBytes[i].ToString("x2"));
				}
				var hashedPassword = sb.ToString();

				return context.Accounts.FirstOrDefault(u => u.UserName == username && u.Password == hashedPassword);
			}
		}
	}
}
