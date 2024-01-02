using EFCoreInMemoryDbDemo;
using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Repositories
{
    public class ShopsRepository : IShopsRepository
    {
        private readonly ApiContext _context;

        public ShopsRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<ShopEntity?> Get(int id)
        {
            return await _context.Shops.FirstOrDefaultAsync(shop => shop.Id == id);
        }

        public async Task<IEnumerable<ShopEntity>> Get()
        {
            return await _context.Shops.ToListAsync();
        }

        public async Task<int> Update(ShopEntity shopEntity)
        {
            _context.Entry(shopEntity).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(ShopEntity shopEntity)
        {
            _context.Shops.Remove(shopEntity);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ShopEntity shopEntity)
        {
            _context.Shops.Add(shopEntity);

            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddItemToShop(ShopEntity shop, ItemEntity item)
        {
            shop.Items.Add(item);

          return await _context.SaveChangesAsync();
        }
    }
}