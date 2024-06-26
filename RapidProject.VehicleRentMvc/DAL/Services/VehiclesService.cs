using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class VehiclesService : IVehicleRepository
    {
        private readonly RentVehicleDbContext _db;
        private readonly VehiclesImageService _imageService;

        public VehiclesService(RentVehicleDbContext db, VehiclesImageService imageService)
        {
            _db = db;
            _imageService = imageService;
        }

        public async Task Add(Vehicle entity)
        {
            try
            {
                await _db.Vehicles.AddAsync(entity);
                await _imageService.Add(entity.VehicleNavigation);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var user = await GetById(id);

                _db.Vehicles.Remove(user);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            var vehicles = await _db.Vehicles.ToListAsync();
            return vehicles;
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
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle> GetById(int id)
        {
            try
            {
                var vehicle = await _db.Vehicles
                        .Include(x => x.VehicleType)
                        .FirstOrDefaultAsync(x => x.VehicleId == id);

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

        public async Task<IEnumerable<Vehicle>> GetVehiclesByName(string name)
        {
            try
            {
                var vehicles = await _db.Vehicles
                    .Include(x => x.VehicleType)
                    .Include(x => x.VehicleNavigation)
                    .Where(x => x.Model == name)
                    .ToListAsync();

                return vehicles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vehicle> Update(Vehicle entity)
        {

            try
            {
                var updatedVehicle = await GetById(entity.VehicleId);
                updatedVehicle.VehicleTypeId = entity.VehicleTypeId;
                updatedVehicle.Make = entity.Make ?? updatedVehicle.Make;
                updatedVehicle.Model = entity.Model ?? updatedVehicle.Model;
                updatedVehicle.Year = entity.Year ?? updatedVehicle.Year;
                updatedVehicle.RentalPrice = entity.RentalPrice;
                updatedVehicle.AvailabilityStatus = entity.AvailabilityStatus;

                await _db.SaveChangesAsync();

                return updatedVehicle;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
