using MVC.DTOs;
using MVC.Models;
using System.Net.Http.Json;
using System.Text.Json;


namespace MVC.Services;

public class ShopService : IShopService
{
    private const string BASE_URL = "http://localhost:5266"; //  Base URL for the API
    
    private readonly HttpClient _httpClient; // The HttpClient used to communicate with the API
    private readonly JsonSerializerOptions _jsonSerializerOptions; // Options used to serialize the data

    public ShopService(HttpClient httpClient) // Constructor that Initializes the service
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
    }

    public IEnumerable<Menu> GetMenus() // get the menus from the API
    {
        var getMenusHttpResponse = _httpClient.GetAsync($"{BASE_URL}/menus").Result; //  Get the response from the API

        if (!getMenusHttpResponse.IsSuccessStatusCode) // If the response is not a success status code returns
        {
            throw new Exception("Something went wrong :("); //  Throw an exception
        }
        using var contentStream = // Get the content
            getMenusHttpResponse.Content.ReadAsStream(); //  Get the content stream from the response

        return JsonSerializer.Deserialize // Deserialize the content
            <IEnumerable<Menu>>(contentStream, _jsonSerializerOptions)!; // Get the menu items from the content stream and deserialize them
    }

    public Order PlaceOrder(IncomingOrder order) // Get the order from the content stream and deserialize them
    {
        var postOrderHttpResponse = _httpClient.PostAsJsonAsync($"{BASE_URL}/orders", order).Result; 

        if (!postOrderHttpResponse.IsSuccessStatusCode) // If the order is not found, throw an exception
        {
            throw new Exception("Something went wrong :(");
        }

        var contentStream = postOrderHttpResponse.Content.ReadAsStream(); //  
        
        return JsonSerializer.Deserialize<Order>(contentStream, _jsonSerializerOptions)!;
    }
}