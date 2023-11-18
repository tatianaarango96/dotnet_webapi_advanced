using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Dtos.VaccineRecord;

namespace MyVaccine.WebApi.Services.Contracts
{
    public interface IVaccineRecordService
    {

        Task<IEnumerable<VaccineRecordResponseDto>> GetAll();
        Task<VaccineRecordResponseDto> GetById(int id);
        Task<VaccineRecordResponseDto> Add(VaccineRecordRequestDto request);
        Task<VaccineRecordResponseDto> Update(VaccineRecordRequestDto request, int id);
        Task<VaccineRecordResponseDto> Delete(int id);
        Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByUserId(int userId);
        Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByDependentId(int dependentId);
        Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByVaccineId(int vaccineId);

    }
}