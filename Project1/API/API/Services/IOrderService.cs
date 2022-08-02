using API.DTOs;
using API.Models;

namespace API.Services;

public interface IOrderService
{
    Task<Order> TakeOrderAsync(IncomingOrder order);
    Task<Order> GetOrderById(Guid id);
}