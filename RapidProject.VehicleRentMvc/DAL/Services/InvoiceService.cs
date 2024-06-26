using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class InvoiceService : IInvoiceRepository
    {
        private readonly RentVehicleDbContext _db;

        public InvoiceService(RentVehicleDbContext db)
        {
            _db = db;
        }

        public async Task Add(Invoice entity)
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var lastInvoice = await _db.Invoices
                    .Where(x => x.InvoiceId.StartsWith($"INV/{currentYear}/{currentMonth}/"))
                    .OrderByDescending(x => x.InvoiceId)
                    .FirstOrDefaultAsync();

            int newIncrement = 1;

            if(lastInvoice != null)
            {
                var lastInvoiceId = lastInvoice.InvoiceId;
                var lastIncrement = int.Parse(lastInvoiceId.Split('/').Last());
                newIncrement = lastIncrement + 1;
            }

            entity.InvoiceId = $"INV/{currentYear}/{currentMonth}/{newIncrement:D4}";

            await _db.Invoices.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Invoice>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Invoice> GetByRentalId(int rentalId)
        {
            try
            {
                var invoice = await _db.Invoices
                .Include(x => x.Rental).ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.RentalId == rentalId);
                return invoice;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public Task<Invoice> Update(Invoice entity)
        {
            throw new NotImplementedException();
        }
    }
}
