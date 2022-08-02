using API.Models;

namespace API.Repositories;

public interface IItemRepository
{
    Task<IList<Item>> GetManyByIdAsync(IEnumerable<Guid> ids); // get item list by id list
}