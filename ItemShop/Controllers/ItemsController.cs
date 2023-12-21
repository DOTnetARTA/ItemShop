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
        public async Task <IActionResult> GetItems()
        {
            return Ok(_itemService.GetItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            return Ok(_itemService.GetItem(id));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            _itemService.DeleteItem(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItem(ItemForUpdateDto item)
        {
            await _itemService.UpdateItem(item);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateItem(ItemForCreateDto item)
        {
            _itemService.CreateItem(item);
            return Ok();
        }
    }
}
