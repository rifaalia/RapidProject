using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class VehiclesImageService : IVehicleImageRepository
    {
        private readonly RentVehicleDbContext _db;
        

        public VehiclesImageService(RentVehicleDbContext db)
        {
            _db = db;
        }
        public async Task Add(VehicleImage entity)
        {
            try
            {
                await _db.VehicleImages.AddAsync(entity);
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
                var image = await GetById(id);
                _db.VehicleImages.Remove(image);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<VehicleImage>> GetAll()
        {
            try
            {
                var images = await _db.VehicleImages.ToListAsync();
                return images;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VehicleImage> GetById(int id)
        {
            try
            {
                var getImage = await _db.VehicleImages.FirstOrDefaultAsync(x => x.VehicleId == id);
                if (getImage == null)
                {
                    throw new Exception("Gambar tidak ditemukan");
                }
                return getImage;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<VehicleImage> Update(VehicleImage entity)
        {
            try
            {
                var image = await GetById(entity.VehicleId);
                image.ImageURL = entity.ImageURL ?? image.ImageURL;
                image.ImageName = entity.ImageName ?? image.ImageName;

                await _db.SaveChangesAsync();

                return image;
            }
            catch (Exception ex)
            {

            throw new Exception(ex.Message); 
            }
        }
    }
}
