using API.Models;

namespace API.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateAsync(Order order); // create order
    Task<Order> GetByIdAsync(Guid id); // get order by id
}