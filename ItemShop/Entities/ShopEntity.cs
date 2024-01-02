namespace ItemShop.Entities
{
    public class ShopEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public ICollection<ItemEntity>? Items { get; set; } = new List<ItemEntity>();

    }
}
