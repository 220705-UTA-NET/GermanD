using MVC.DTOs;
using MVC.Models;

namespace MVC.Services;

public interface IShopService 
{
    IEnumerable<Menu> GetMenus(); // returns all menus
    Order PlaceOrder(IncomingOrder order); // returns the order
}