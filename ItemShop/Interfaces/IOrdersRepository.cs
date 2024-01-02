using ItemShop.Entities;

namespace ItemShop.Interfaces
{
    public interface IOrdersRepository
    {
        Task<OrdersEntity> Add(OrdersEntity order);
    }
}