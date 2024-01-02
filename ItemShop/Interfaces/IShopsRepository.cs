using ItemShop.Entities;

namespace ItemShop.Interfaces
{
    public interface IShopsRepository
    {
        Task<int>AddItemToShop(ShopEntity shop, ItemEntity item);
        Task<int> Create(ShopEntity shopEntity);
        Task<int> Delete(ShopEntity shopEntity);
        Task<IEnumerable<ShopEntity>> Get();
        Task<ShopEntity?> Get(int id);
        Task<int> Update(ShopEntity shopEntity);
    }
}