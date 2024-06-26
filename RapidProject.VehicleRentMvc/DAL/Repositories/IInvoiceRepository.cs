using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.DAL.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
       Task<Invoice> GetByRentalId(int rentalId);
    }
}
