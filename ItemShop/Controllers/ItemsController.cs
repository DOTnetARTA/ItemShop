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
        public IActionResult GetItems()
        {
            return Ok(_itemService.GetItems());
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            return Ok(_itemService.GetItem(id));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {

            return Ok(_itemService.DeleteItem(id));
        }
        [HttpPut]
        public IActionResult UpdateItem(ItemForUpdateDto item)
        {

            return Ok(_itemService.UpdateItem(item));
        }
        [HttpPost]
        public IActionResult CreateItem(ItemForCreateDto item)
        {

            return Ok(_itemService.CreateItem(item));
        }
    }
}
