using ItemShop.Client;
using ItemShop.Dtos;
using ItemShop.Interfaces;

namespace ItemShop.Services
{
    public class UsersService
    {
        private readonly IJsonPlaceholderClient _placeholderClient;

        public UsersService(IJsonPlaceholderClient jsonPlaceholderClient)
        {
            _placeholderClient = jsonPlaceholderClient;
        }

        public async Task<List<UsersForGetDto>?> GetUsers()
        {
            var usersEntities = await _placeholderClient.GetUsers();

            if (!usersEntities.IsSuccessful)
            {
                throw new Exception(usersEntities.ErrorMessage);
            }

            var usersDto = usersEntities.Data?.Select(userEntity => new UsersForGetDto
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email
            }).ToList();

            return usersDto;
        }

        public async Task<UsersForGetDto> GetUser(int id)
        {
            var usersEntity = await _placeholderClient.GetUser(id);

            if (!usersEntity.IsSuccessful)
            {
                throw new Exception(usersEntity.ErrorMessage);
            }

            UsersForGetDto userDto = new UsersForGetDto
            {
                Id = usersEntity.Data!.Id,
                Name = usersEntity.Data.Name,
                Email = usersEntity.Data.Email
            };

            return userDto;
        }

        public async Task<UserForCreateDto?> CreateUser(UserForCreateDto user)
        {
            var result = await _placeholderClient.CreateUser(user);

            if (!result.IsSuccessful)
            {
                throw new Exception(result.ErrorMessage);
            }

            return result.Data;
        }
    }
}
