using FlightStatusApp.Context;
using FlightStatusApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightStatusApp.Repositories;

public class FlightRepository : IRepository
{
    private readonly FlightContext _context;

    public FlightRepository(FlightContext context)
    {
        _context = context;
    }

    public async Task<Flight> GetStatus(int id)
    {
        return await _context.Statuses.FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task<IEnumerable<Flight>> GetAllStatuses()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<IEnumerable<Flight>> GetFilteredStatuses(string origin, string destination)
    {
        IQueryable<Flight> statuses = _context.Statuses;
        if (!string.IsNullOrEmpty(origin))
            statuses = statuses.Where(s => s.Origin == origin);
        if (!string.IsNullOrEmpty(destination))
            statuses = statuses.Where(s => s.Destination == destination);
        return await statuses.ToListAsync();
    }

    public async Task<Flight> Create(Flight status)
    {
        await _context.Statuses.AddAsync(status);
        Save();
        return status;
    }

    public Task<Flight> Update(Flight status)
    {
        _context.Statuses.Update(status);
        Save();
        return Task.FromResult(status);
    }

    public async void Save()
    {
       await _context.SaveChangesAsync();
    }
}