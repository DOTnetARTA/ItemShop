using ItemShop.Dtos;
using ItemShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _usersService.GetUsers());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _usersService.GetUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserForCreateDto user)
        {
            return Ok(await _usersService.CreateUser(user));
        }
    }
}
