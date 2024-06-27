using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RapidProject.VehicleRentMvc.DAL.Repositories;
using RapidProject.VehicleRentMvc.DAL.Services;
using RapidProject.VehicleRentMvc.ViewModels;
using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentService;
        private readonly IInvoiceRepository _invoiceService;
        private readonly IRentRepository _rentService;

        public PaymentController(IPaymentRepository paymentService,
                IInvoiceRepository invoiceService,
                IRentRepository rentService)
        {
            _paymentService = paymentService;
            _invoiceService = invoiceService;
            _rentService = rentService;
        }

        // GET: PaymentController
        public async Task<IActionResult> Index()
        {
            var payment = await _paymentService.GetAll();
            return View(payment);
        }

        // GET: PaymentController/Details/5
        //[HttpGet("payment/{id}")]
        //public async Task<IActionResult> Details(int id)
        //{
        //    var payment = await _paymentService.GetById(id);
        //    if (payment == null)
        //    {
        //        return NotFound();
        //    }

        //    var rentalViewModel = new RentalViewModel
        //    {
        //        VehicleId = vehicle.VehicleId,
        //        VehicleMake = vehicle.Make,
        //        VehicleModel = vehicle.Model
        //    };

        //    return View(rentalViewModel);
        //}

        // GET: PaymentController/Create
        public async Task <IActionResult> Create()
        {
            ViewBag.Invoice = await _invoiceService.GetAll();
            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                await _paymentService.Add(payment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Invoice = await _invoiceService.GetAll();
            return View(payment);

            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: PaymentController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _paymentService.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewBag.Invoice = await _invoiceService.GetAll();
            return View(payment);
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(int id, Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _paymentService.Update(payment);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Invoice = await _invoiceService.GetAll();
            return View(payment);
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: PaymentController/Delete/5
        public async Task <IActionResult> Delete(int id)
        {
            var payment = await _paymentService.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: PaymentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _paymentService.Delete(id);
            return RedirectToAction(nameof(Index));
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}
