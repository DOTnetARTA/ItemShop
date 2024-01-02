using ItemShop.Entities;

namespace ItemShop.Dtos
{
    public class UsersForGetDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }
        public ICollection<ItemEntity>? Items { get; set; } = new List<ItemEntity>();

    }
}
