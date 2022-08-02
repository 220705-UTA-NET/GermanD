using MVC.Models;

namespace MVC.DTOs;

public class OrderItem
{
    public Item? Item { get; set; } // the item ordered
    public uint Quantity { get; set; } // the quantity of the ordered items
}