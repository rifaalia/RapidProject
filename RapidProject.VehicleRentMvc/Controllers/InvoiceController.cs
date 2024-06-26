using Microsoft.AspNetCore.Mvc;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.ViewModels;

namespace RapidProject.VehicleRentMvc.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceService;

        public InvoiceController(IInvoiceRepository invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("Invoice/Rental/{rentalId}")]
        public async Task<IActionResult> Index(int rentalId)
        {

            var invoice = await _invoiceService.GetByRentalId(rentalId);

            if (invoice == null)
            {
                return NotFound();  
            }

            

            return View(invoice);
        }
    }
}
