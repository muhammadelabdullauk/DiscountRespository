namespace Moneysupermarket.Model
{
    public class BasketLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
