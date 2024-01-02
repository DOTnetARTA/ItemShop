
using Dapper;
using EFCoreInMemoryDbDemo;
using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;
using static Dapper.SqlMapper;

namespace ItemShop.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApiContext _context;
        public ItemRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<ItemEntity?> Get(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(item => item.Id == id);

        }

        public async Task<IEnumerable<ItemEntity>> Get()
        {
            return await _context.Items.ToListAsync();

        }

        public async Task<int> Update(ItemEntity itemEntityt)
        {
            _context.Entry(itemEntityt).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(ItemEntity itemEntity)
        {
            _context.Items.Remove(itemEntity);

            return await _context.SaveChangesAsync();

        }

        public async Task<int> Create(ItemEntity itemEntity)
        {
            _context.Items.Add(itemEntity);

            return await _context.SaveChangesAsync();
        }

        public async Task<ItemEntity?> GetItemByShopId(int itemId, int shopId)
        {
            ItemEntity? item = await _context.Items
                .Include(i => i.Shop)
                .FirstOrDefaultAsync(i => i.Id == itemId && i.ShopId == shopId);

            return item;
        }
    }
}

