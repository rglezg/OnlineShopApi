using System;

namespace OnlineShop.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}