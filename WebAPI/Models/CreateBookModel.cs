﻿namespace WebAPI.Models;

public class CreateBookModel
{
    public string Isbn { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ICollection<int> AuthorIds { get; set; }
    public int PublisherId { get; set; }
}