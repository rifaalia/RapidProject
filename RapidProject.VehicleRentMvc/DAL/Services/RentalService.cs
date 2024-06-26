using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class RentalService : IRentRepository
    {
        private readonly RentVehicleDbContext _db;
        public RentalService(RentVehicleDbContext db)
        {
            _db = db;
        }
        public async Task Add(Rental entity)
        {
            var newRent = new Rental
            {
                VehicleId = entity.VehicleId,
                UserId = entity.UserId,
                RentalDate = entity.RentalDate,
                ReturnDate = entity.ReturnDate,
            };
            try
            {
                await _db.Rentals.AddAsync(newRent);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Rental>> GetAll()
        {
            var getAlLRents = await _db.Rentals
                .Include(x => x.Vehicle)
                .Include(x => x.User)
                .ToListAsync();

            return getAlLRents;
        }

        public Task<Rental> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Rental> Update(Rental entity)
        {
            throw new NotImplementedException();
        }
    }
}
