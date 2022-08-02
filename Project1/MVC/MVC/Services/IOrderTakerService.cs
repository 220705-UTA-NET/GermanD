using MVC.DTOs;

namespace MVC.Services;

public interface IOrderTakerService
{
    IncomingOrder TakeOrder(); // checks if the order is nothing, if not, it takes the order
}