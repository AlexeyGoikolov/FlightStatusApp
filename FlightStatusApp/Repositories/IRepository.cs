using FlightStatusApp.Models;

namespace FlightStatusApp.Repositories;

public interface IRepository
{
    public IEnumerable<Flight> GetAllStatuses();
    public IQueryable<Flight> GetFilteredStatuses(string origin, string destination);
    public void Create(Flight status);
    public void Update(Flight status);
    public void Save();


}