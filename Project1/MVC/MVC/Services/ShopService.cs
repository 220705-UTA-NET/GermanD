using MVC.DTOs;
using MVC.Models;

namespace MVC.Services;

public class ShopService : IShopService
{
    public IEnumerable<Menu> GetMenus()
    {
        return new[]
        {
            new Menu
            {
                Id = Guid.NewGuid(),
                Name = "Food Menu",
                Items = new[]
                {
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Hamburger",
                        Price = 12.99m,
                        Type = ItemType.FOOD,
                    },
                },
            },
            new Menu
            {
                Name = "Drink Menu",
                Items = new []
                {
                    new Item
                    {
                        Id = Guid.NewGuid(),
                        Name = "Coca Cola",
                        Price = 1.99m,
                        Type = ItemType.DRINK,
                    }
                }
            },
        };
    }

    public Order PlaceOrder(IncomingOrder order)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            Items = order.Items,
            CustomerName = order.CustomerName,
            Total = order.Items.Sum(n => n.Item.Price * n.Quantity),
        };
    }
}