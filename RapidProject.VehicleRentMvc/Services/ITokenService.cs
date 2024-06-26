using RapidProject.VehicleRentMvc.Models;

namespace RapidProject.VehicleRentMvc.Services
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
