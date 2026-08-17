using Domain.Models;

namespace Application.Services;

public interface IExampleService
{
    Task<Example> CreateAsync(string name);
    Task<IEnumerable<Example>> GetAllAsync();
}
