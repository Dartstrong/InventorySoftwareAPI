using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KGTA_project.Models;
namespace KGTA_project.Interfaces
{
    public interface IItemsRepository
    {
        Task<ActionResult<IEnumerable<Item?>>> GetAllItems();//вернуть все записи
        Task<ActionResult<IEnumerable<Item?>>> GetAllCabinetItems(string Cabinet);//вернуть все записи в кабинете
        Task<ActionResult<IEnumerable<string?>>> GetAllCabinets();//вернуть все имеющиеся кабинеты
        Task<ActionResult<Item?>> CreateItem(Item item);//создание записи
        Task<ActionResult<Item?>> GetItem(int id);//поиск записи по первичному ключу
        Task<ActionResult<Item?>> UpdateItem(Item item);//обновление записи
        Task<ActionResult<Item?>> DeleteItem(int id);//удаление записи по первичному ключу
        Task<ActionResult<IEnumerable<Item?>>> CreateEmptyItems(int number);//создание пустых записей
        Task<ActionResult<IEnumerable<Item?>>> GetAllEmptyItems();//вернуть все пустые записи
    }
}
