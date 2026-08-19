using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User> Add(UserDto userDto)
    {
        var user = new User(userDto.Username, userDto.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUser(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
}