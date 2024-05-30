using System.Threading.Tasks;
using KGTA_project.Models;
using KGTA_project.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace KGTA_project.Controllers
{
    [Route("api/accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountsRepository _accountsRepository;
        public AccountsController(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }
        [HttpGet("{login}/{password}")]
        public async Task<ActionResult<Account?>> GetAccount(string login, string password)
        {
            if (await _accountsRepository.GetAccount(login, password) == null)
            {
                return BadRequest();
            }
            return await _accountsRepository.GetAccount(login, password);
        }
        [HttpGet("all/{login}/{password}")]
        public async Task<ActionResult<IEnumerable<Account?>>> GetAllAccounts(string login, string password)
        {
            if (await _accountsRepository.GetAllAccounts(login, password) == null)
            {
                return BadRequest();
            }
            return await _accountsRepository.GetAllAccounts(login, password);
        }
        [HttpPost]
        public async Task<ActionResult<Account?>> CreateItem([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }
            return await _accountsRepository.CreateAccount(account);
        }
        [HttpPut("{login}/{password}")]
        public async Task<ActionResult<Account?>> UpdateAccount(string login, string password, [FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest();
            }

            var updatedAccount = await _accountsRepository.UpdateAccount(login,password,account);

            if (updatedAccount == null)
            {
                return NotFound();
            }

            return updatedAccount;
        }
        [HttpDelete("{login}/{password}")]
        public async Task<ActionResult<Account?>> DeleteAccount(string login, string password, [FromBody] Account account)
        {
            var deletedAccount = await _accountsRepository.DeleteAccount(login,password,account);

            if (deletedAccount == null)
            {
                return BadRequest();
            }
            return deletedAccount;
        }
    }
}
