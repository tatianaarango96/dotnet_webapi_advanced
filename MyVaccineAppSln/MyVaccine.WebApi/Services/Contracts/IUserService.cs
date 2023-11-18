using MyVaccine.WebApi.Dtos;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IUserService
    {

        Task<VaccineRecordRequestDto> AddUserAsync(RegisterRequetDto request);
        Task<VaccineRecordRequestDto> Login (LoginRequestDto request);
        Task<VaccineRecordRequestDto> RefreshToken(string email);
        Task<User> GetUserInfo(string email);

    }
}
