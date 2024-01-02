using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Services
{
    public class ShopsService
    {
        private readonly IShopsRepository _shopRepository;
        private readonly IItemRepository _itemRepository;

        public ShopsService(IShopsRepository shopRepository, IItemRepository itemRepository)
        {
            _shopRepository = shopRepository;
            _itemRepository = itemRepository;
        }

        public async Task<ShopEntity> Get(int id)
        {
            return await _shopRepository.Get(id) ?? throw new NotFoundException();
        }

        public async Task<IEnumerable<ShopsForGetDto>> Get()
        {
            var shops = await _shopRepository.Get();

            var shopsDto = shops.Select(shop => new ShopsForGetDto
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address
            });

            return shopsDto.Any() ? shopsDto : Enumerable.Empty<ShopsForGetDto>();
        }

        public async Task Update(ShopForUpdateDto shop)
        {
            var shopEntity = await Get(shop.Id) ?? throw new NotFoundException();

            shopEntity.Name = shop.Name;
            shopEntity.Address = shop.Address;

            var result = await _shopRepository.Update(shopEntity);

            if (result == 0)
            {
                throw new Exception("Update failed");
            }
        }

        public async Task Create(ShopForCreateDto shop)
        {
            var shopEntity = new ShopEntity
            {
                Name = shop.Name,
                Address = shop.Address,
            };

            var result = await _shopRepository.Create(shopEntity);

            if (result == 0)
            {
                throw new Exception("Creation failed");
            }
        }

        public async Task Delete(int id)
        {
            var shopEntity = await Get(id) ?? throw new NotFoundException();

            var result = await _shopRepository.Delete(shopEntity);

            if (result == 0)
            {
                throw new Exception("Deletion failed");
            }
        }

        public async Task AddItemToShop(int shopId, int itemId)
        {
            var shop = await _shopRepository.Get(shopId);

            if (shop == null)
            {
                throw new NotFoundException("Shop not found");
            }

            var item = await _itemRepository.Get(itemId);

            if (item == null)
            {
                throw new NotFoundException("Item not found");
            }

            if (shop.Items.Any(i => i.Id == item.Id))
            {
                throw new Exception("Item is already associated with the shop");
            }

            var result = await _shopRepository.AddItemToShop(shop, item);

            if (result == 0)
            {
                throw new Exception("Add Shop to item failed");
            }
        }

    }
}