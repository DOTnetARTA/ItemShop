using FluentAssertions;
using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Repositories;
using ItemShop.Services;
using Moq;

namespace ItemShopTests
{
    public class ItemServicesTest
    {
        private readonly ItemService _itemService;
        private readonly Mock<IItemRepository> _mockItemRepository;

        public ItemServicesTest()
        {
            _mockItemRepository = new Mock<IItemRepository>();
            _itemService = new ItemService(_mockItemRepository.Object);
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsItemEntity()
        {
            // Arrange
            int validId = 1;
            var expectedItemEntity = new ItemEntity { Id = validId };

            _mockItemRepository.Setup(repo => repo.Get(validId)).ReturnsAsync(expectedItemEntity);

            // Act
            var result = await _itemService.Get(validId);

            //Assert
            result.Id.Should().Be(validId);
        }

        [Fact]
        public async Task Get_WithInvalidId_ReturnsNotFoundException()
        {
            var invalidId = -1;

            _mockItemRepository.Setup(repo => repo.Get(invalidId)).ReturnsAsync((ItemEntity?)null);

            Func<Task> act = async () => await _itemService.Get(invalidId);

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
        }

        [Fact]
        public async Task Get_WhenItemsExists_ReturnsItems()
        {
            // Arrange
            var expectedItems = new List<ItemEntity>
            {
                new ItemEntity { Id = 1, Name = "Item1" },
                new ItemEntity { Id = 2, Name = "Item2" }
            };


            // Act
            var result = await _itemService.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task Get_WhenItemsNotExists_ReturnsNotFoundException()
        {
            // Arange
            var emptyList = new List<ItemEntity>();

            _mockItemRepository.Setup(repo => repo.Get()).ReturnsAsync(emptyList);

            // Act
            Func<Task> act = async () => await _itemService.Get();

            // Assert 
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");

        }

        [Fact]
        public async Task Update_WithValidItem_UpdatesItemSuccessfully()
        {
            var itemId = 1;
            var itemToUpdate = new ItemForUpdateDto { Id = itemId, Name = "UpdatedItem", Price = 19.99M };

            var existingItem = new ItemEntity { Id = itemId, Name = "OriginalItem", Price = 9.99M };

            _mockItemRepository.Setup(repo => repo.Get(itemId)).ReturnsAsync(existingItem);
            _mockItemRepository.Setup(repo => repo.Update(It.IsAny<ItemEntity>())).ReturnsAsync(1);

            // Act
            Func<Task> act = async () => await _itemService.Update(itemToUpdate);

            // Assert
            await act.Should().NotThrowAsync<Exception>();
           
            _mockItemRepository.Verify(repo => repo.Update(It.IsAny<ItemEntity>()), Times.Once);
            
            existingItem.Name.Should().Be(itemToUpdate.Name); 
            existingItem.Price.Should().Be(itemToUpdate.Price);
        }

        [Fact]
        public async Task Update_WithNonExistingItem_ThrowsNotFoundException()
        {
            // Arrange
            var itemId = 1;
            var itemToUpdate = new ItemForUpdateDto { Id = itemId, Name = "UpdatedItem", Price = 19.99M };

            _mockItemRepository.Setup(repo => repo.Get(itemId)).ReturnsAsync((ItemEntity)null); 

            // Act
            Func<Task> act = async () => await _itemService.Update(itemToUpdate);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>(); 
            _mockItemRepository.Verify(repo => repo.Update(It.IsAny<ItemEntity>()), Times.Never); 
        }

        [Fact]
        public async Task Update_WhenRepositoryFailsToUpdate_ThrowsException()
        {
            // Arrange
            var itemId = 1;
            var itemToUpdate = new ItemForUpdateDto { Id = itemId, Name = "UpdatedItem", Price = 19.99M };

            var existingItem = new ItemEntity { Id = itemId, Name = "OriginalItem", Price = 9.99M };

            _mockItemRepository.Setup(repo => repo.Get(itemId)).ReturnsAsync(existingItem);
            _mockItemRepository.Setup(repo => repo.Update(It.IsAny<ItemEntity>())).ReturnsAsync(0); 

            // Act
            Func<Task> act = async () => await _itemService.Update(itemToUpdate);

            // Assert
            await act.Should().ThrowAsync<Exception>();
            _mockItemRepository.Verify(repo => repo.Update(It.IsAny<ItemEntity>()), Times.Once); 
        }
    }
}


