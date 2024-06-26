using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class UsersService : IUserRepository
    {
        private readonly RentVehicleDbContext _db;

        public UsersService(RentVehicleDbContext db)
        {
            _db = db;
        }

        public async Task Add(User entity)
        {
            try
            {
                await _db.Users.AddAsync(entity);

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

                _db.Users.Remove(user);
                
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                var users = await _db.Users.ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetById(int id)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(x => x.UserId == x.UserId);
                
                if(user == null)
                {
                    throw new Exception("User tidak ditemukan");
                }

                return user;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetUsersByName(string name)
        {
            try
            {
                var users = await GetUsersWithProfile();

                var filterdUsers = users.Where(x => x.FullName.Contains(name));
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<IEnumerable<User>> GetUsersWithProfile()
        {
            var users = await _db.Users.ToListAsync();

            return users;
        }

        public async Task<User> Update(User entity)
        {
            try
            {
                var updatedUser = await GetById(entity.UserId);

                updatedUser.Email = entity.Email;
                updatedUser.Password = entity.Password;
                updatedUser.PhoneNumber = entity.PhoneNumber;
                updatedUser.Role = entity.Role;

                await _db.SaveChangesAsync();
                return updatedUser;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
