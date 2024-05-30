using System.Collections.Generic;
using System.Threading.Tasks;
using KGTA_project.Models;
using KGTA_project.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace KGTA_project.Controllers
{
    [Route("api/items")]
    public class ItemsController : Controller
    {
        private readonly IItemsRepository _itemsRepository;
        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        [HttpGet(Name = "GetAllItems")]
        public async Task<ActionResult<IEnumerable<Item?>>> GetAllItems()
        {
            return await _itemsRepository.GetAllItems();
        }

        [HttpGet("Empty")]
        public async Task<ActionResult<IEnumerable<Item?>>> GetAllEmptyItems()
        {
            return await _itemsRepository.GetAllEmptyItems();
        }

        [HttpGet("Cabinet/{Cabinet}")]
        public async Task<ActionResult<IEnumerable<Item?>>> GetAllCabinetItems(string Cabinet)
        {
            return await _itemsRepository.GetAllCabinetItems(Cabinet);
        }

        [HttpGet("Cabinet/all")]
        public async Task<ActionResult<IEnumerable<string?>>> GetAllCabinets()
        {
            return await _itemsRepository.GetAllCabinets();
        }

        [HttpGet("{id}", Name = "GetItem")]
        public async Task<ActionResult<Item?>> GetItem(int id)
        {
            if (await _itemsRepository.GetItem(id) == null)
            {
                return NotFound();
            }
            return await _itemsRepository.GetItem(id);
        }

        [HttpPost]
        public async Task<ActionResult<Item?>> CreateItem([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }     
            return await _itemsRepository.CreateItem(item);
        }

        [HttpPost("{number}")]
        public async Task<ActionResult<IEnumerable<Item?>>> CreateEmptyItems(int number)
        {
            if(number==0)
            {
                return BadRequest();
            }
            return await _itemsRepository.CreateEmptyItems(number);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Item?>> UpdateItem(int id, [FromBody] Item updatedItem)
        {
            if (updatedItem == null || updatedItem.Id != id)
            {
                return BadRequest();
            }

            var item = await _itemsRepository.UpdateItem(updatedItem);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Item?>> DeleteItem(int id)
        {
            var deletedItem = await _itemsRepository.DeleteItem(id);

            if (deletedItem == null)
            {
                return BadRequest();
            }
            return deletedItem;
        }
    }
}   
