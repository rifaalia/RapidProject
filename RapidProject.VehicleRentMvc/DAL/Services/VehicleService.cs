using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class VehicleService : IVehicleRepository
    {
        private readonly RentVehicleDbContext _db;
        public VehicleService(RentVehicleDbContext db)
        {
            _db = db;
        }
        public async Task Add(Vehicle entity)
        {
            
            await _db.Vehicles.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var vehicle = await GetById(id);
                _db.Remove(vehicle);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            try
            {
                var vehicles = await _db.Vehicles.Include(x => x.VehicleType).ToListAsync();
                return vehicles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesByType(string typeName)
        {
            try
            {
                var vehicles = await _db.Vehicles
                .Include(x => x.VehicleType)
                .Where(x => x.VehicleType.VehicleType1 == typeName)
                .ToListAsync();

                return vehicles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle> GetById(int id)
        {
            try
            {
                var vehicle = await _db.Vehicles.FirstOrDefaultAsync(x => x.VehicleId == id);

                if (vehicle == null)
                {
                    throw new Exception("Kendaraan tidak ditemukan");
                }
                return vehicle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehicleAvailable()
        {
            try
            {
                var vehicles = await _db.Vehicles.Include(x => x.VehicleType).Where(x => x.AvailabilityStatus == 1).ToListAsync();

                return vehicles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByMake(string name)
        {
            try
            {
                var vehicles = await _db.Vehicles.Where(x => x.Make.Contains(name)).ToListAsync();
                return vehicles;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);

            }            
        }

        public async Task<Vehicle> Update(Vehicle entity)
        {
            try
            {
                var vehicle = await GetById(entity.VehicleId);
                vehicle.Make = entity.Make;
                vehicle.VehicleTypeId = entity.VehicleTypeId;
                vehicle.Model = entity.Model;
                vehicle.Year = entity.Year;
                vehicle.RentalPrice = entity.RentalPrice;
                vehicle.AvailabilityStatus = entity.AvailabilityStatus;

                await _db.SaveChangesAsync();
                return vehicle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
