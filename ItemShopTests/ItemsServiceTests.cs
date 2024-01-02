using AutoFixture;
using FluentAssertions;
using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Exceptions;
using ItemShop.Interfaces;
using ItemShop.Services;
using Moq;

namespace ItemShopTests
{
    public class ItemsServiceTests
    {
        private readonly ItemsService _itemService;
        private readonly Mock<IItemRepository> _mockItemRepository;
        private readonly Mock<IJsonPlaceholderClient> _mockJsonPlaceholderClient;
        private readonly Mock<IShopsRepository> _mockShopsRepository;
        private readonly Mock<IOrdersRepository> _mockOrdersRepository;

        private readonly IFixture _fixture;

        public ItemsServiceTests()
        {
            _fixture = new Fixture();
            _mockItemRepository = new Mock<IItemRepository>();
            _mockJsonPlaceholderClient = new Mock<IJsonPlaceholderClient>();
            _mockShopsRepository = new Mock<IShopsRepository>();
            _mockOrdersRepository = new Mock<IOrdersRepository>();

            _itemService = new ItemsService(
                _mockItemRepository.Object,
                _mockJsonPlaceholderClient.Object,
                _mockShopsRepository.Object,
                _mockOrdersRepository.Object
            );
        }

        [Fact]
        public async Task Get_WithValidId_ReturnsItemEntity()
        {
            // Arrange
            int validId = _fixture.Create<int>();
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
            // Arrange
            var invalidId = _fixture.Create<int>();

            _mockItemRepository.Setup(repo => repo.Get(invalidId)).ReturnsAsync((ItemEntity?)null);

            // Act
            Func<Task> act = async () => await _itemService.Get(invalidId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Not found");
        }

        [Fact]
        public async Task Get_WhenItemsExists_ReturnsItems()
        {
            // Arrange
            var expectedItems = _fixture.Build<ItemEntity>().Without(ie => ie.Shop).CreateMany(5).ToList();
            _mockItemRepository.Setup(repo => repo.Get()).ReturnsAsync(expectedItems);

            // Act
            var result = await _itemService.Get();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedItems);
        }

        [Fact]
        public async Task Get_WhenItemsNotExists_ReturnsEmptyArray()
        {
            // Arange
            var emptyList = new List<ItemEntity>();

            _mockItemRepository.Setup(repo => repo.Get()).ReturnsAsync(emptyList);

            // Act
            var result = await _itemService.Get();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Update_WithValidItem_UpdatesItemSuccessfully()
        {
            // Arrange 
            var itemId = _fixture.Create<int>();
            var itemToUpdate = _fixture.Build<ItemForUpdateDto>().With(item => item.Id, itemId).Create();
            var existingItem = _fixture.Build<ItemEntity>().Without(ie => ie.Shop).Create();

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
            var itemId = _fixture.Create<int>();
            var itemToUpdate = _fixture.Build<ItemForUpdateDto>().With(x => x.Id, itemId).Create();

            _mockItemRepository.Setup(repo => repo.Get(itemId)).ReturnsAsync((ItemEntity?)null);

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
            var itemId = _fixture.Create<int>();

            var itemToUpdate = _fixture.Build<ItemForUpdateDto>().With(x => x.Id, itemId).Create();

            var existingItem = _fixture.Build<ItemEntity>().Without(ie => ie.Shop).Create();

            _mockItemRepository.Setup(repo => repo.Get(itemId)).ReturnsAsync(existingItem);
            _mockItemRepository.Setup(repo => repo.Update(It.IsAny<ItemEntity>())).ReturnsAsync(0);

            // Act
            Func<Task> act = async () => await _itemService.Update(itemToUpdate);

            // Assert
            await act.Should().ThrowAsync<Exception>();
            _mockItemRepository.Verify(repo => repo.Update(It.IsAny<ItemEntity>()), Times.Once);
        }
        [Fact]
        public async Task Create_ValidItemForCreateDto_ItemEntitySuccessfullyCreated()
        {
            // Arrange
            var itemForCreateDto = _fixture.Create<ItemForCreateDto>();

            _mockItemRepository.Setup(repo => repo.Create(It.IsAny<ItemEntity>())).ReturnsAsync(1);

            // Act
            Func<Task> act = async () => await _itemService.Create(itemForCreateDto);

            // Assert
            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task Create_FailedItemEntityCreation_ThrowsException()
        {
            // Arrange
            var itemForCreateDto = _fixture.Create<ItemForCreateDto>();

            _mockItemRepository.Setup(repo => repo.Create(It.IsAny<ItemEntity>())).ReturnsAsync(0);

            // Act
            Func<Task> act = async () => await _itemService.Create(itemForCreateDto);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task Delete_ExistingItemId_ItemEntitySuccessfullyDeleted()
        {
            // Arrange
            var itemId = _fixture.Create<int>();
            var existingItem = _fixture.Build<ItemEntity>().Without(ie => ie.Shop).Create();

            _mockItemRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(existingItem);
            _mockItemRepository.Setup(repo => repo.Delete(It.IsAny<ItemEntity>())).ReturnsAsync(1);

            // Act
            Func<Task> act = async () => await _itemService.Delete(itemId);

            // Assert
            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task Delete_NonExistingItemId_ThrowsNotFoundException()
        {
            // Arrange
            var itemId = _fixture.Create<int>();

            _mockItemRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((ItemEntity?)null);

            // Act
            Func<Task> act = async () => await _itemService.Delete(itemId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Delete_FailedItemEntityDeletion_ThrowsException()
        {
            // Arrange
            var itemId = _fixture.Create<int>();
            var existingItem = _fixture.Build<ItemEntity>().Without(ie => ie.Shop).Create();

            _mockItemRepository.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync(existingItem);
            _mockItemRepository.Setup(repo => repo.Delete(It.IsAny<ItemEntity>())).ReturnsAsync(0);

            // Act
            Func<Task> act = async () => await _itemService.Delete(itemId);

            // Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }

}
