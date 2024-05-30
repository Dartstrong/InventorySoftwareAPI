using System.Linq;
using System.Threading.Tasks;
using KGTA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace KGTA_project.Interfaces
{
    public class EFAccountsRepository : IAccountsRepository
    {
        private readonly Context _context;
        public EFAccountsRepository(Context context)
        {
            _context = context;
        }
        public async Task<ActionResult<Account?>> GetAccount(string login, string password)
        {
            var selectedAccounts = await _context.Accounts.Where(account => account.Login == login).ToListAsync();
            foreach(var account in selectedAccounts)
            {
                if(account.Password==password)
                {
                    return account;
                }                  
            }
            return null;
        }
        public async Task<ActionResult<IEnumerable<Account?>>> GetAllAccounts(string login, string password)
        {
            var selectedAccount = await _context.Accounts.Where(account => account.Login == login).ToListAsync();
            if (selectedAccount != null)
            {
                if ((selectedAccount[0].Password == password) && (selectedAccount[0].Rights == "system administrator"))
                { 
                    return await _context.Accounts.ToListAsync();
                }      
            }
            return null;
        }

        public async Task<ActionResult<Account?>> CreateAccount(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }
        public async Task<ActionResult<Account?>> UpdateAccount(string login, string password, Account account)
        {
            var selectedAccount = await _context.Accounts.Where(account => account.Login == login).ToListAsync();
            var updatedAccount = await _context.Accounts.FindAsync(account.Id);
            if (selectedAccount != null)
            {
                if ((selectedAccount[0].Password == password)&&(selectedAccount[0].Rights == "system administrator"))
                {     
                    updatedAccount.Login = account.Login;
                    updatedAccount.Password = account.Password;
                    updatedAccount.Rights = account.Rights;
                    _context.Accounts.Update(updatedAccount);
                    await _context.SaveChangesAsync();
                }              
            }
            return updatedAccount;
        }
        public async Task<ActionResult<Account?>> DeleteAccount(string login, string password, Account account)
        {
            var selectedAccount = await _context.Accounts.Where(account => account.Login == login).ToListAsync();

            if (selectedAccount != null)
            {
                if((selectedAccount[0].Password==password)&& (selectedAccount[0].Rights == "system administrator"))
                {
                    _context.Accounts.Remove(account);
                    await _context.SaveChangesAsync();
                }
            }
            return account;
        }
    }
}
