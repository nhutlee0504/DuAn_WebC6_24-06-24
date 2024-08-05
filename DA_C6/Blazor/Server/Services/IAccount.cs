using Microsoft.AspNetCore.Mvc;
using Blazor.Server.Data;
using Blazor.Shared.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Server.Services
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
