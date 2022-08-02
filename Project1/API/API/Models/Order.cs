namespace API.Models;

public class Order
{
    public Guid? Id { get; set; } = null;
    public string? CustomerName { get; set; } // name in of person who ordered
    public IList<OrderItem> Items { get; set; } = new List<OrderItem>(); // items ordered by customer

    public decimal Total => Items.Sum(n => n.Quantity * n.Item.Price); // total price of order
}