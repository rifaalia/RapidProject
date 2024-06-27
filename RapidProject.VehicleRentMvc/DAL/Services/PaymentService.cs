using Microsoft.EntityFrameworkCore;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Services
{
    public class PaymentService : IPaymentRepository
    {
        private readonly RentVehicleDbContext _db;

        public PaymentService(RentVehicleDbContext db)
        {
            _db = db;
        }
        public async Task Add(Payment entity)
        {
            await _db.Payments.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var payment = await GetById(id);
                _db.Remove(payment);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            try
            {
                var payment = await _db.Payments.Include(x => x.PaymentId).ToListAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> GetById(int id)
        {
            try
            {
                var payment = await _db.Payments.FirstOrDefaultAsync(x => x.PaymentId == id);

                if (payment == null)
                {
                    throw new Exception("Pembayaran tidak ditemukan");
                }
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Payment> Update(Payment entity)
        {
            try
            {
                var payment = await GetById(entity.PaymentId);
                payment.InvoiceId = entity.InvoiceId;
                payment.Amount = entity.Amount;
                payment.InvoiceDate = entity.InvoiceDate;
                
                await _db.SaveChangesAsync();
                return payment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
