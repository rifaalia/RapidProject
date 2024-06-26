using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class VehicleTypeService : IVehicleTypeRepository
    {
        private readonly RentVehicleDbContext _db;

        public VehicleTypeService(RentVehicleDbContext db)
        {
            _db = db;
        }

        public async Task Add(VehicleType entity)
        {
            try
            {
                await _db.VehicleTypes.AddAsync(entity);
                await _db.SaveChangesAsync();
            }
            catch
            (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var vehicleType = await GetById(id);

                _db.VehicleTypes.Remove(vehicleType);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<VehicleType>> GetAll()
        {
            try
            {
                var vehicleTypes = await _db.VehicleTypes.ToListAsync();

                return vehicleTypes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VehicleType> GetById(int id)
        {
            try
            {
                var vehicleType = await _db.VehicleTypes.FirstOrDefaultAsync(x => x.VehicleTypeId == id);
                if (vehicleType == null)
                {
                    throw new Exception("Tipe Kendaraan tidak ditemukan");
                }

                return vehicleType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VehicleType> Update(VehicleType entity)
        {
            try
            {
                var vehicleType = await GetById(entity.VehicleTypeId);
                vehicleType.VehicleType1 = entity.VehicleType1 ?? vehicleType.VehicleType1;

                await _db.SaveChangesAsync();
                return vehicleType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
