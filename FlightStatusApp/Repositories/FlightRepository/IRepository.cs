using FlightStatusApp.Models;

namespace FlightStatusApp.Repositories;

public interface IRepository
{
    public Task<Flight> GetStatus(int id);
    public Task<IEnumerable<Flight>> GetAllStatuses();
    public Task<IEnumerable<Flight>> GetFilteredStatuses(string origin, string destination);
    public Task<Flight> Create(Flight status);
    public Task<Flight> Update(Flight status);
    public void Save();


}