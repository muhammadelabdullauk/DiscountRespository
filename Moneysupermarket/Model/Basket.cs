using System.Collections.Generic;

namespace Moneysupermarket.Model
{
    public class Basket
    {
        public Basket()
        {
            BasketLines = new List<BasketLine>();
        }

        public IEnumerable<BasketLine> BasketLines { get; set; }

    }
}
