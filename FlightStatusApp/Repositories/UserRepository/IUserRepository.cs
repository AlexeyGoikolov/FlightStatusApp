using FlightStatusApp.Models;

namespace FlightStatusApp.Repositories;

public interface IUserRepository
{
    public Task<User> GetUser(string name);
}