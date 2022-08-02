using API.DTOs;
using API.Models;
using API.Repositories;

namespace API.Services;

public class OrderService : IOrderService
{
    private readonly IItemRepository _itemRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IItemRepository itemRepository, IOrderRepository orderRepository) // Injection of repository
    {
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
    }

    public async Task<Order> TakeOrderAsync(IncomingOrder order)
    {
        if (!await DoAllItemsExist(order.Items.Select(n => n.Item.Id)))
        {
            throw new Exception("One or more items were not found");
        }

        return await _orderRepository.CreateAsync(MapIncomingOrderToOrder(order));
    }

    public Task<Order> GetOrderById(Guid id) => _orderRepository.GetByIdAsync(id);

    private Order MapIncomingOrderToOrder(IncomingOrder incomingOrder)
    {
        var items = incomingOrder.Items.Select(n => new OrderItem
        {
            Item = n.Item,
            Quantity = n.Quantity
        });

        var orderItems = items.ToList();
        return new Order
        {
            Items = orderItems,
            CustomerName = incomingOrder.CustomerName,
        };
    }

    private async Task<bool> DoAllItemsExist(IEnumerable<Guid> itemIds)
    {
        // TODO: Get the exact items that were not found by getting the difference between what was returned and what was expected
        var enumerable = itemIds.ToList();
        var items = await _itemRepository.GetManyByIdAsync(enumerable);

        return items.Count == enumerable.Count;
    }
}