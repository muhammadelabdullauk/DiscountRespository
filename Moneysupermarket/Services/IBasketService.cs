using Moneysupermarket.Model;

namespace Moneysupermarket.Services
{
    public interface IBasketService
    {
        decimal CalculateTotalValue(Basket basket);
    }
}
