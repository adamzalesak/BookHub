﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Order : BaseEntity
    {
        public String Email { get; set; }
        public String Address { get; set; }
        public long Phone { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderState State { get; set; }
        public DateTime Timestamp { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}