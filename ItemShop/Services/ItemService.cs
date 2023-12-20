using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Repositories;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Services
{
    public class ItemService
    {
        private readonly ItemRepository _itemRepository;

        public ItemService(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ItemEntity GetItem(int id)
        {
            return _itemRepository.GetItem(id);
        }


        public IEnumerable<ItemEntity> GetItems()
        {
            var items = _itemRepository.GetItems();

            return items.Any() ? items : throw new Exception("Item not found");
        }

        public int UpdateItem(ItemForUpdateDto item)
        {
            var itemEntity = GetItem(item.Id);

            if (itemEntity != null)
            {
                itemEntity.Price = item.Price;
                itemEntity.Name = item.Name;

                int result = _itemRepository.UpdateItem(itemEntity);

                return result != 0
                    ? result
                    : throw new InvalidOperationException("Update failed. Item not found or no changes were made.");

            }

            throw new NullReferenceException("Item not found");
        }

        public int CreateItem(ItemForCreateDto item)
        {
            var itemEntity = new ItemEntity
            {
                Name = item.Name,
                Price = item.Price,
            };

            int result = _itemRepository.CreateItem(itemEntity);

            return result != 0
                   ? result
                   : throw new InvalidOperationException("Create failed. Item not found or no changes were made.");
        }

        public int DeleteItem(int id)
        {
            var itemEntity = GetItem(id);

            if (itemEntity != null)
            {
                int result = _itemRepository.DeleteItem(itemEntity);

                return result != 0
                    ? result
                    : throw new InvalidOperationException("Delete failed. Item not found or no changes were made.");

            }

            throw new NullReferenceException("Item not found");
        }

    }
}
