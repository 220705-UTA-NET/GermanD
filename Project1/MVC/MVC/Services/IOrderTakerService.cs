using MVC.DTOs;

namespace MVC.Services;

public interface IOrderTakerService
{
    IncomingOrder TakeOrder();
}