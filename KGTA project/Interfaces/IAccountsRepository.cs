using KGTA_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace KGTA_project.Interfaces
{
    public interface IAccountsRepository
    {
        Task<ActionResult<Account?>> GetAccount(string login, string password);//авторизация
        Task<ActionResult<IEnumerable<Account?>>> GetAllAccounts(string login, string password);//получение информации обо всех аккаунтах
        Task<ActionResult<Account?>> CreateAccount(Account account);//добавление пользователя
        Task<ActionResult<Account?>> UpdateAccount(string login, string password, Account account);//обновление данных пользователя
        Task<ActionResult<Account?>> DeleteAccount(string login, string password, Account account);//удаление пользователя
    }
}
