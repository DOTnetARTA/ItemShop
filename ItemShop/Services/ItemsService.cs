using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Repositories;
using System.Security.Cryptography;

namespace ItemShop.Services
{
    public class ItemsService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IJsonPlaceholderClient _jsonPlaceholderClient;
        private readonly IShopsRepository _shopsRepository;
        private readonly IOrdersRepository _ordersRepository;

        public ItemsService(IItemRepository itemRepository,
            IJsonPlaceholderClient jsonPlaceholderClient,
            IShopsRepository shopsRepository,
            IOrdersRepository ordersRepository
            )
        {
            _itemRepository = itemRepository;
            _jsonPlaceholderClient = jsonPlaceholderClient;
            _shopsRepository = shopsRepository;
            _ordersRepository = ordersRepository;
        }

        public async Task<ItemEntity> Get(int id)
        {
            return await _itemRepository.Get(id) ?? throw new NotFoundException();
        }


        public async Task<IEnumerable<ItemEntity>> Get()
        {

            var items = await _itemRepository.Get();

            return items.Any() ? items : Enumerable.Empty<ItemEntity>();

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

        public async Task<OrdersEntity> Buy(int itemId, ItemToBuyDto data)
        {
            var user = await _jsonPlaceholderClient.GetUser(data.UserId);

            if (!user.IsSuccessful)
            {
                throw new NotFoundException("User not found");
            }

            var shop = await _shopsRepository.Get(data.ShopId);

            if (shop == null)
            {
                throw new NotFoundException("Shop not found");
            }

            var shopItem = await _itemRepository.GetItemByShopId(itemId, data.ShopId);

            if (shopItem == null)
            {
                throw new NotFoundException("Shop with item not found");
            }

            OrdersEntity order = new OrdersEntity
            {
                UserId = user.Data.Id,
                ShopId = shopItem.ShopId,
                ItemId = shopItem.Id
            };

            return await _ordersRepository.Add(order);
        }
    }
}

