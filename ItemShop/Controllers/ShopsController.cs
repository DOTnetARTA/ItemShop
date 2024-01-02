using ItemShop.Dtos;
using ItemShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly ShopsService _shopsService;

        public ShopsController(ShopsService shopsService)
        {
            _shopsService = shopsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _shopsService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _shopsService.Get(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _shopsService.Delete(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ShopForUpdateDto shop)
        {
            await _shopsService.Update(shop);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ShopForCreateDto shop)
        {
            await _shopsService.Create(shop);

            return Ok();
        }
        [HttpPost("{shopId}/items/{itemId}")]
        public async Task<IActionResult> AddItemToShop(int shopId, int itemId)
        {
            await _shopsService.AddItemToShop(shopId, itemId);

            return Ok();
        }
    }
}
