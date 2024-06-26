namespace RapidProject.VehicleRentMvc.ViewModels
{
    public class InvoiceViewModel
    {
        public string InvoiceId { get; set; }
        public int RentalId { get; set; }
        public string CustomerName { get; set; }
        public  string PhoneNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string VehicleYear { get; set; }
        public string VehicleType { get; set; }
        public decimal Amount { get; set; }

        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
