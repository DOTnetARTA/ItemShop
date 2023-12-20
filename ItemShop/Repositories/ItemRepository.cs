
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

        public ItemEntity GetItem(int id)
        {
            return _context.Items.FirstOrDefault(item => item.Id == id) ?? throw new NullReferenceException("Item not found Null returned");
        }

        public IEnumerable<ItemEntity> GetItems()
        {
            return _context.Items.ToList();
        }

        public int UpdateItem(ItemEntity itemEntityt)
        {
            _context.Entry(itemEntityt).State = EntityState.Modified;

            return _context.SaveChanges();
        }

        public int DeleteItem(ItemEntity itemEntity)
        {
            _context.Items.Remove(itemEntity);

            return _context.SaveChanges();

        }

        public int CreateItem(ItemEntity itemEntity)
        {
            _context.Items.Add(itemEntity);

            return _context.SaveChanges();
        }
    }
}

