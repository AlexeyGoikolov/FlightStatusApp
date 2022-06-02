using FlightStatusApp.Context;
using FlightStatusApp.Models;

namespace FlightStatusApp.Repositories;

public class FlightRepository : IRepository
{
    private readonly FlightContext _context;

    public FlightRepository(FlightContext context)
    {
        _context = context;
    }

    public IEnumerable<Flight> GetAllStatuses()
    {
        return _context.Statuses;
    }

    public IQueryable<Flight> GetFilteredStatuses(string origin, string destination)
    {
        IQueryable<Flight> statuses = _context.Statuses;
        if (!string.IsNullOrEmpty(origin))
            statuses = statuses.Where(s => s.Origin == origin);
        if (!string.IsNullOrEmpty(destination))
            statuses = statuses.Where(s => s.Destination == destination);
        return statuses;
    }

    public void Create(Flight status)
    {
        _context.Statuses.Add(status);
        Save();
    }

    public void Update(Flight status)
    {
        _context.Statuses.Update(status);
        Save();
    }

    public async void Save()
    {
       await _context.SaveChangesAsync();
    }
}