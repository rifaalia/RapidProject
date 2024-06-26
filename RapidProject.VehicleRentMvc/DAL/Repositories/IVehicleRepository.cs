using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetVehiclesByName(string name);
        Task<IEnumerable<Vehicle>> GetAllVehiclesByType(string typeName);
    }
}
