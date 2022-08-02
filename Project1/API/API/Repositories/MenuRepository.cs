using System.Data.SqlClient;
using API.Models;
using Dapper;

namespace API.Repositories;

public class MenuRepository : Repository, IMenuRepository
{
    public MenuRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IList<Menu>> GetAllAsync() // get menu list
    {
        await using var connection = new SqlConnection(_connectionString); // create connectionString

        var result = await connection.QueryAsync<Item, Menu, Menu>(@" 
SELECT 
    i.ID, i.NAME, i.PRICE, i.TYPE,
    m.ID, m.NAME
    FROM [Menu] m 
        JOIN [Menu_Item] mi 
            ON m.ID=mi.MENU_ID
        JOIN [Item] i
            ON i.ID = mi.ITEM_ID",
            (item, menu) => // join item and menu
            {
                menu.Items.Add(item); // add item to menu
                return menu; // return menu
            },
            splitOn: "Id"); // split on menu id

        return result
            .GroupBy(n => n.Id)
            .Select(n => n.Aggregate(new Menu(), (acc, curr) =>
            {
                acc.Name = curr.Name;
                acc.Id = curr.Id;
                foreach (var item in curr.Items)
                {
                    acc.Items.Add(item);
                }

                return acc;
            })).AsList();
    }
    //    [1,2,3,4,5].Aggregate(0, (acc, curr) => acc + curr);
}