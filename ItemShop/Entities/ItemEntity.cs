namespace ItemShop.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }    
        public decimal Price { get; set; }
        public int? ShopId {  get; set; }
        public ShopEntity Shop { get; set; }
    }
}
