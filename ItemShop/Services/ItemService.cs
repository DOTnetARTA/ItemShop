using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task <ItemEntity> GetItem(int id)
        {
            return await _itemRepository.GetItem(id);
        }


        public IEnumerable<ItemEntity> GetItems()
        {
            var items = _itemRepository.GetItems();

            return items.Any() ? items : throw new NotFoundException();
        }

        public async Task UpdateItem(ItemForUpdateDto item)
        {
            try
            {
                var itemEntity = await GetItem(item.Id);

                if (itemEntity == null)
                {
                    throw new NotFoundException();
                }

                itemEntity.Price = item.Price;
                itemEntity.Name = item.Name;

                await _itemRepository.UpdateItem(itemEntity);

            }
            catch (SqlException ex)
            {
                throw new SqlException("Update item", ex);
            }

        }

        public void CreateItem(ItemForCreateDto item)
        {
            try
            {
                var itemEntity = new ItemEntity
                {
                    Name = item.Name,
                    Price = item.Price,
                };

                _itemRepository.CreateItem(itemEntity);

            }
            catch (SqlException ex)
            {
                throw new SqlException("CreateItem", ex);
            }

        }

        public void DeleteItem(int id)
        {
            try
            {
                var itemEntity = GetItem(id);

                if (itemEntity == null)
                {
                    throw new NotFoundException();

                }

                _itemRepository.DeleteItem(itemEntity);
            }
            catch (SqlException ex)
            {
                throw new SqlException("DeleteItem", ex);
            }

        }

    }
}
