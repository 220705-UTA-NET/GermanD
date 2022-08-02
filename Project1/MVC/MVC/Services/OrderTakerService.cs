using System.ComponentModel.DataAnnotations;
using MVC.DTOs;
using MVC.Models;
using MVC.Services;
using Sharprompt;

namespace MVC;

public class OrderTaker : IOrderTakerService // OrderTaker is a child class of IOrderTaker Service 
{
    private readonly IShopService _shopService; 

    public OrderTaker(IShopService shopService) // OrderTaker constructor
  
    {
        _shopService = shopService; 
    }

    public IncomingOrder TakeOrder() // 
    {
        var orderItems = new List<OrderItem>(); //  List of OrderItems

        do
        {
            orderItems.Add(TakeItem()); //  Add an OrderItem to the list
        }
        while (Prompt.Confirm("Do You Want To Order Another Item?"));

        var customerName = GetCustomerName(); // Get the customer name

        return new IncomingOrder
        {
            Items = orderItems, 
            CustomerName = customerName,
        };
    }

    private OrderItem TakeItem()
    {
        var selectedMenu = SelectMenu(); //  Select a menu

        var selectedItem = SelectItem(selectedMenu); //  Select an item from the menu

        var selectedQuantity = SelectQuantity(selectedItem); //  Select a quantity for the item

        return new OrderItem
        {
            Item = selectedItem, //  Set the item
            Quantity = selectedQuantity, //  Set the quantity
        };
    }

    private string GetCustomerName() => Prompt.Input<string>("Please Enter Your Name"); //  Get the customer name

    private Menu SelectMenu() =>
        Prompt.Select(
            "Menus",
            _shopService.GetMenus(),
            textSelector: menu => $"{menu.Name}"); //  Set the text selector to display the menu name

    private Item SelectItem(Menu menu) => Prompt.Select("Menu Items", menu.Items); // Select an item from the menu

    private uint SelectQuantity(Item selectedItem) => //  Select a quantity for the item
        uint.Parse(
            Prompt.Input<string>(
                $"How Many {selectedItem.Name}?", // The name of the item
                validators: new[] {
                    Validators.Required(),
                    Validators.RegularExpression(@"^\d+$", "Please enter a number greater than 0"), // From start to end, needs to be digits at least once but can be many
                    input => uint.TryParse(input.ToString(), out var parsedValue) && parsedValue > 0 ? ValidationResult.Success : new ValidationResult("Please enter a a number greater than 0"),
                }
        ));
}