using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ExampleRepository : IExampleRepository
{
    private readonly AppDbContext _context;

    public ExampleRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Example?> GetByIdAsync(Guid id) =>
        _context.Examples.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<IEnumerable<Example>> GetAllAsync() =>
        await _context.Examples.ToListAsync();

    public async Task AddAsync(Example example)
    {
        _context.Examples.Add(example);
        await _context.SaveChangesAsync();
    }
}
