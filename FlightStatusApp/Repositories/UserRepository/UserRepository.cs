using FlightStatusApp.Context;
using FlightStatusApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightStatusApp.Repositories;

public class UserRepository :IUserRepository
{
    private readonly FlightContext _context;

    public UserRepository(FlightContext context)
    {
        _context = context;
    }
    
    public async Task<User> GetUser(string name)
    {
        return await _context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.Username == name);
    }
}