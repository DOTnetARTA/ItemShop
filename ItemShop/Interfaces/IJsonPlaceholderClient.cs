using ItemShop.Dtos;
using ItemShop.Entities;

namespace ItemShop.Interfaces
{
    public interface IJsonPlaceholderClient
    {
        Task<JsonPlaceHolderResult<UserForCreateDto>> CreateUser(UserForCreateDto user);
        Task<JsonPlaceHolderResult<UsersEntity>> GetUser(int id);
        Task<JsonPlaceHolderResult<List<UsersEntity>>> GetUsers();
    }
}