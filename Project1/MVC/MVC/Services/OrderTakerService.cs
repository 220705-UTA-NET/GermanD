using System.ComponentModel.DataAnnotations;
using MVC.DTOs;
using MVC.Models;
using MVC.Services;
using Sharprompt;

namespace MVC;

public class OrderTaker : IOrderTakerService
{
    private readonly IShopService _shopService;

    public OrderTaker(IShopService shopService)
    {
        _shopService = shopService;
    }

    public IncomingOrder TakeOrder()
    {
        var orderItems = new List<OrderItem>();

        do
        {
            orderItems.Add(TakeItem());
        }
        while (Prompt.Confirm("Do You Want To Order Another Item?"));

        var customerName = GetCustomerName();

        return new IncomingOrder
        {
            Items = orderItems,
            CustomerName = customerName,
        };
    }

    private OrderItem TakeItem()
    {
        var selectedMenu = SelectMenu();

        var selectedItem = SelectItem(selectedMenu);

        var selectedQuantity = SelectQuantity(selectedItem);

        return new OrderItem
        {
            Item = selectedItem,
            Quantity = selectedQuantity,
        };
    }

    private string GetCustomerName() => Prompt.Input<string>("Please Enter Your Name");

    private Menu SelectMenu() =>
        Prompt.Select(
            "Menus",
            _shopService.GetMenus(),
            textSelector: menu => $"{menu.Name}");

    private Item SelectItem(Menu menu) => Prompt.Select("Menu Items", menu.Items);

    private uint SelectQuantity(Item selectedItem) =>
        uint.Parse(
            Prompt.Input<string>(
                $"How Many {selectedItem.Name}?",
                validators: new[] {
                    Validators.Required(),
                    Validators.RegularExpression(@"^\d+$", "Please enter a number greater than 0"), // From start to end, needs to be digits at least once but can be many
                    input => uint.TryParse(input.ToString(), out var parsedValue) && parsedValue > 0 ? ValidationResult.Success : new ValidationResult("Please enter a a number greater than 0"),
                }
        ));
}