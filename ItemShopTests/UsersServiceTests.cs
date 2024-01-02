using AutoFixture;
using FluentAssertions;
using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Interfaces;
using ItemShop.Services;
using Moq;

namespace ItemShopTests
{
    public class UsersServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IJsonPlaceholderClient> _placeholderClientMock;
        private readonly UsersService _userService;

        public UsersServiceTests()
        {
            _fixture = new Fixture();
            _placeholderClientMock = new Mock<IJsonPlaceholderClient>();
            _userService = new UsersService(_placeholderClientMock.Object);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUserDto_WhenSuccessful()
        {
            // Arrange
            int userId = _fixture.Create<int>();
            var expectedUserEntity = _fixture.Build<UsersEntity>().With(u => u.Id, userId).Create();

            _placeholderClientMock.Setup(c => c.GetUser(It.IsAny<int>()))
                .ReturnsAsync(new JsonPlaceHolderResult<UsersEntity>
                {
                    IsSuccessful = true,
                    Data = expectedUserEntity
                });

            // Act
            var userDto = await _userService.GetUser(userId);

            // Assert
            userDto.Should().BeEquivalentTo(expectedUserEntity, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task GetUser_ShouldThrowException_WhenNotSuccessful()
        {
            // Arrange
            int userId = _fixture.Create<int>();
            var errorMessage = _fixture.Create<string>();

            _placeholderClientMock.Setup(c => c.GetUser(It.IsAny<int>()))
                .ReturnsAsync(new JsonPlaceHolderResult<UsersEntity>
                {
                    IsSuccessful = false,
                    ErrorMessage = errorMessage
                });

            // Act 
            Func<Task> act = async () => await _userService.GetUser(userId);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage(errorMessage);
        }
    }
}
