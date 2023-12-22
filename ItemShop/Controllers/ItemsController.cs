using ItemShop.Dtos;
using ItemShop.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _itemService.Get());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _itemService.Get(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.Delete(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ItemForUpdateDto item)
        {
            await _itemService.Update(item);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemForCreateDto item)
        {
            await _itemService.Create(item);

            return Ok();
        }
    }
}
