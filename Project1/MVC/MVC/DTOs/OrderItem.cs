using MVC.Models;

namespace MVC.DTOs;

public class OrderItem
{
    public Item Item { get; set; }
    public uint Quantity { get; set; }
}