using System.Data.SqlClient;
using API.Models;
using Dapper;

namespace API.Repositories;

public class ItemRepository : Repository, IItemRepository // extends repository, implements ItemRepository
{
    public ItemRepository(IConfiguration configuration) : base(configuration) // constructor

    {
    }

    public async Task<IList<Item>> GetManyByIdAsync(IEnumerable<Guid> ids) // get item list with id
    {
        await using var connection = new SqlConnection(_connectionString); // create connectionString

        var result = await connection.QueryAsync<Item>( // query item list with id
            @"SELECT * FROM Item WHERE ID IN @ids",
            new { ids }
        );

        return result.ToList(); // return item list
    }
}