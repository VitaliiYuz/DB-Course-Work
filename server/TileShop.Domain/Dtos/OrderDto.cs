﻿namespace TileShop.Domain.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetailsDto> Details { get; set; } = new();
}
