namespace ItemShop.Entities
{
    public class OrdersEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int? ShopId { get; set; }

        public int UserId { get; set; }
    }
}
