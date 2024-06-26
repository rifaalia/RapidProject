using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {         
        Task<IEnumerable<Vehicle>> GetVehiclesByMake(string make);
        Task<IEnumerable<Vehicle>> GetAllVehiclesByType(string typeName);
        Task<IEnumerable<Vehicle>> GetVehicleAvailable();
        Task<Vehicle> UpdateStatus(int id);
    }
}
