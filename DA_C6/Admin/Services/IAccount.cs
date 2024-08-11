using Admin.Model;
using System.Collections.Generic;

namespace Admin.Services
{
	public interface IAccount
	{
		public IEnumerable<Account> GetAccounts();
		public Account GetAccountById(string user);
		public Account AddAccount(Account account);
		public Account UpdateAccount(string user, Account account);
		public void DeleteAccount(string user);
		public Account LoginAccount(string username, string password);
	}
}
