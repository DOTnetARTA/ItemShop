using ItemShop.Entities;

namespace ItemShop.Interfaces
{
    public interface IItemRepository
    {
        Task<int> Create(ItemEntity itemEntity);
        Task<int> Delete(ItemEntity itemEntity);
        Task<ItemEntity?> Get(int id);
        Task<IEnumerable<ItemEntity>>   Get();
        Task <ItemEntity?> GetItemByShopId(int itemId, int shopId);
        Task<int> Update(ItemEntity itemEntityt);
    }
}