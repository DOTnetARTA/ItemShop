using ItemShop.Entities;

namespace ItemShop.Repositories
{
    public interface IItemRepository
    {
        Task<int> Create(ItemEntity itemEntity);
        Task<int> Delete(ItemEntity itemEntity);
        Task<ItemEntity?> Get(int id);
        Task<IEnumerable<ItemEntity>> Get();
        Task<int> Update(ItemEntity itemEntityt);
    }
}