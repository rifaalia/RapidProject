namespace RapidProject.VehicleRentMvc.ViewModels
{
    public class RentalViewModel
    {
        public int VehicleId { get; set; }
        public int UserId { get; set; }
        public string? VehicleMake { get; set; }
        public string? VehicleModel { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
    }
}
