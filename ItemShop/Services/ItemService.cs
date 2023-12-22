using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Repositories;

namespace ItemShop.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ItemEntity> Get(int id)
        {
            return await _itemRepository.Get(id) ?? throw new NotFoundException();
        }


        public async Task<IEnumerable<ItemEntity>> Get()
        {

            var items = await _itemRepository.Get();

            return items.Any() ? items : throw new NotFoundException();

        }

        public async Task Update(ItemForUpdateDto item)
        {
            var itemEntity = await Get(item.Id) ?? throw new NotFoundException();

            itemEntity.Price = item.Price;
            itemEntity.Name = item.Name;

            var result = await _itemRepository.Update(itemEntity);

            if (result == 0)
            {
                throw new Exception();
            }
        }

        public async Task Create(ItemForCreateDto item)
        {
            var itemEntity = new ItemEntity
            {
                Name = item.Name,
                Price = item.Price,
            };

            var result = await _itemRepository.Create(itemEntity);

            if (result == 0)
            {
                throw new Exception();
            }
        }

        public async Task Delete(int id)
        {
            var itemEntity = await Get(id) ?? throw new NotFoundException();

            var result = await _itemRepository.Delete(itemEntity);

            if (result == 0)
            {
                throw new Exception();
            }
        }
    }
}

