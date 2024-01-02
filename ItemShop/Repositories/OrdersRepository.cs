using EFCoreInMemoryDbDemo;
using ItemShop.Entities;
using ItemShop.Interfaces;

namespace ItemShop.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ApiContext _context;

        public OrdersRepository(ApiContext apiContext)
        {
            _context = apiContext;
        }

        public async Task<OrdersEntity> Add(OrdersEntity order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
