﻿using DataAccessLayer.Models;

namespace WebAPI.Models.Ordering
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderState State { get; set; }
        public DateTime Timestamp { get; set; }
        public int CartId { get; set; }
        public int? UserId { get; set; }
    }
}
