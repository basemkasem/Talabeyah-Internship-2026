using Application.Interfaces;
using Domain.Models;

namespace Application.Services;

public class ExampleService : IExampleService
{
    private readonly IExampleRepository _repository;

    public ExampleService(IExampleRepository repository)
    {
        _repository = repository;
    }

    public async Task<Example> CreateAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        var example = new Example(Guid.NewGuid(), name);
        await _repository.AddAsync(example);
        return example;
    }

    public Task<IEnumerable<Example>> GetAllAsync() => _repository.GetAllAsync();
}
