using Domain.Models;

namespace Application.Interfaces;

public interface IExampleRepository
{
    Task<Example?> GetByIdAsync(Guid id);
    Task<IEnumerable<Example>> GetAllAsync();
    Task AddAsync(Example example);
}
