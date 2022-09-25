using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingCard
    {
        public string UserName { get; set; }
        public List<ShoppingCardItem> Items { get; set; } = new List<ShoppingCardItem>();

        public ShoppingCard()
        {

        }
        public ShoppingCard(string userName)
        {
            this.UserName = userName;  
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;
                foreach(ShoppingCardItem item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
