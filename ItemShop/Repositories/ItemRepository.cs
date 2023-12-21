
using Dapper;
using EFCoreInMemoryDbDemo;
using ItemShop.Dtos;
using ItemShop.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace ItemShop.Repositories
{
    public class ItemRepository
    {
        private readonly ApiContext _context;
        public ItemRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task <ItemEntity> GetItem(int id)
        {
             return await _context.Items.FirstOrDefaultAsync(item => item.Id == id) ?? throw new NullReferenceException("Item not found Null returned");
        }

        public async Task<IEnumerable<ItemEntity>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task UpdateItem(ItemEntity itemEntityt)
        {
            _context.Entry(itemEntityt).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(ItemEntity itemEntity)
        {
            _context.Items.Remove(itemEntity);

             await _context.SaveChangesAsync();

        }

        public async Task CreateItem(ItemEntity itemEntity)
        {
            _context.Items.Add(itemEntity);

            await _context.SaveChangesAsync();
        }
    }
}

