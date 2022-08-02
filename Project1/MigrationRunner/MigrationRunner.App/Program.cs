// See https://aka.ms/new-console-template for more information
using Dapper;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

var connectString = "False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

using(var connection = new SqlConnection(connectString))
{
    var items = connection.Query(@"select * from Item").AsList();

    var foodMenu = Guid.Parse("5c1aaaea-ef86-4db6-850a-c5d7afa103f2");
    var drinkMenu = Guid.Parse("1e911c29-fa06-43ba-99ae-065c088d0ebc");

    connection.Execute(@"INSERT INTO [Menu_Item](Menu_ID, ITEM_ID)
    VALUES(@MenuId, @Itemid)",

    items.Select(n => new
    {
        MenuID = n.TYPE == 0 ? //turnary operator (3 parts)
        drinkMenu : foodMenu, // if item type = 0 goes into drinkMenu if not go into food
        ItemId = n.ID 
    }

    ));
    
    
}