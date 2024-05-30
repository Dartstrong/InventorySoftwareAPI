using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KGTA_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace KGTA_project.Interfaces
{
    public class EFItemsRepository : IItemsRepository
    {
        private Context _context;
        public EFItemsRepository(Context context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Item?>>> GetAllItems()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Item?>>> GetAllCabinetItems(string Cabinet)
        {
            var selectedItems = await _context.Items.Where(item => item.Cabinet == Cabinet).ToListAsync();
            return selectedItems;
        }
        public async Task<ActionResult<IEnumerable<string?>>> GetAllCabinets()
        {
            var selectedCabinets = await _context.Items.Select(item => item.Cabinet).Distinct().ToListAsync();
            return selectedCabinets;
        }

        public async Task<ActionResult<Item?>> CreateItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<ActionResult<Item?>> GetItem(int id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task<ActionResult<Item?>> UpdateItem(Item updatedItem)
        {
            var selectedItem = await _context.Items.FindAsync(updatedItem.Id);

            if (selectedItem != null)
            {
                selectedItem.Number = updatedItem.Number;
                selectedItem.Specification = updatedItem.Specification;
                selectedItem.Cabinet = updatedItem.Cabinet;
                selectedItem.Data = updatedItem.Data;
                selectedItem.WODate = updatedItem.WODate;
                selectedItem.ReasonWO = updatedItem.ReasonWO;
                selectedItem.Responsible = updatedItem.Responsible;

                _context.Items.Update(selectedItem);
                await _context.SaveChangesAsync();
            }
            return selectedItem;
        }

        public async Task<ActionResult<Item?>> DeleteItem(int id)
        {
            var selectedItem = await _context.Items.FindAsync(id);

            if (selectedItem != null)
            {
                _context.Items.Remove(selectedItem);
                await _context.SaveChangesAsync();
            }
            return selectedItem;
        }

        public async Task<ActionResult<IEnumerable<Item?>>> CreateEmptyItems(int number)
        {
            Item[] items = new Item[number];
            for (int i = 0; i < number; i++) items[i] = new Item();
            _context.Items.AddRange(items);
            await _context.SaveChangesAsync();
            return items;
        }

        public async Task<ActionResult<IEnumerable<Item?>>> GetAllEmptyItems()
        {
            return await _context.Items.Where(item => item.Cabinet==null).ToListAsync();
        }
    }
}
