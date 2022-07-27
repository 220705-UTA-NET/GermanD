using MVC.DTOs;
using MVC.Models;

namespace MVC.Services;

public interface IShopService
{
    IEnumerable<Menu> GetMenus();
    Order PlaceOrder(IncomingOrder order);
}