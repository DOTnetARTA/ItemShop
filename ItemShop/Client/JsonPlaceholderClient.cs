using ItemShop.Dtos;
using ItemShop.Entities;
using ItemShop.Interfaces;
namespace ItemShop.Client
{
    public class JsonPlaceholderClient : IJsonPlaceholderClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string UsersUrl;

        public JsonPlaceholderClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            UsersUrl = _configuration.GetValue<string>("ExternalApi:jsonPlaceHolder:Users") ?? throw new Exception("Api url is null");
        }


        public async Task<JsonPlaceHolderResult<List<UsersEntity>>> GetUsers()
        {
            var response = await _httpClient.GetAsync(UsersUrl);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UsersEntity>>();

                return new JsonPlaceHolderResult<List<UsersEntity>>
                {
                    Data = result,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceHolderResult<List<UsersEntity>>
                {
                    Data = null,
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }

        public async Task<JsonPlaceHolderResult<UsersEntity>> GetUser(int id)
        {
            var response = await _httpClient.GetAsync($"{UsersUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UsersEntity>();

                return new JsonPlaceHolderResult<UsersEntity>
                {
                    Data = result,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceHolderResult<UsersEntity>
                {
                    Data = null,
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }

        public async Task<JsonPlaceHolderResult<UserForCreateDto>> CreateUser(UserForCreateDto user)
        {
            var response = await _httpClient.PostAsJsonAsync(UsersUrl, user);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<UserForCreateDto>();

                return new JsonPlaceHolderResult<UserForCreateDto>
                {
                    Data = result,
                    IsSuccessful = true,
                    ErrorMessage = null
                };
            }
            else
            {
                return new JsonPlaceHolderResult<UserForCreateDto>
                {
                    Data = null,
                    IsSuccessful = false,
                    ErrorMessage = response.StatusCode.ToString()
                };
            }
        }

    }
}
