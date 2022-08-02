using API.Models;

namespace API.Repositories;

public interface IMenuRepository
{
    Task<IList<Menu>> GetAllAsync(); // get all menu list
}