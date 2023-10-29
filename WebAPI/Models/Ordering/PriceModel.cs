﻿namespace WebAPI.Models.Ordering;

public class PriceModel
{
    public int Id { get; set; }
    public decimal BookPrice { get; set; }
    public DateTime ValidFrom { get; set; }
    public int BookId { get; set; }
}