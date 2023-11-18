using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using AutoMapper;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Dtos.Dependent;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class VaccineRecordService : IVaccineRecordService
    {

        private readonly IBaseRepository<VaccineRecord> _VaccineRecordRepository;
        private readonly IMapper _mapper;
        public VaccineRecordService(IBaseRepository<VaccineRecord> vaccineRecordRepository, IMapper mapper)
        {
            _VaccineRecordRepository = vaccineRecordRepository;
            _mapper = mapper;
        }

        public async Task<VaccineRecordResponseDto> Add(VaccineRecordRequestDto request)
        {
            var vaccineRecord = new VaccineRecord();
            vaccineRecord.UserId = request.UserId;
            vaccineRecord.DependentId = request.DependentId;
            vaccineRecord.VaccineId = request.VaccineId;
            vaccineRecord.DateAdministered = request.DateAdministered;
            vaccineRecord.AdministeredLocation = request.AdministeredLocation;
            vaccineRecord.AdministeredBy = request.AdministeredBy;


            await _VaccineRecordRepository.Add(vaccineRecord);
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecord);
            return response;
        }

        public async Task<VaccineRecordResponseDto> Delete(int id)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();

            await _VaccineRecordRepository.Delete(vaccineRecord);
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecord);
            return response;
        }

        public async Task<IEnumerable<VaccineRecordResponseDto>> GetAll()
        {
            var vaccineRecord = await _VaccineRecordRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccineRecord);
            return response;
        }

        public async Task<VaccineRecordResponseDto> GetById(int id)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindByAsNoTracking(x => x.VaccineRecordId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecord);
            return response;
        }

        public async Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByVaccineRecordId(int vaccineRecordId)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindByAsNoTracking(x => x.VaccineRecordId == vaccineRecordId).ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccineRecord);
            return response;
        }

        public async Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByUserId(int userId)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindByAsNoTracking(x => x.UserId == userId).ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccineRecord);
            return response;
        }

        public async Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByVaccineId(int vaccineId)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindByAsNoTracking(x => x.VaccineId == vaccineId).ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccineRecord);
            return response;
        }

        public async Task<IEnumerable<VaccineRecordResponseDto>> GetVaccineRecordByDependentId(int dependentId)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindByAsNoTracking(x => x.DependentId == dependentId).ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineRecordResponseDto>>(vaccineRecord);
            return response;
        }
        public async Task<VaccineRecordResponseDto> Update(VaccineRecordRequestDto request, int id)
        {
            var vaccineRecord = await _VaccineRecordRepository.FindBy(x => x.VaccineRecordId == id).FirstOrDefaultAsync();
            vaccineRecord.UserId = request.UserId;
            vaccineRecord.VaccineRecordId = request.VaccineRecordId;
            vaccineRecord.VaccineId = request.VaccineId;
            vaccineRecord.DateAdministered = request.DateAdministered;
            vaccineRecord.AdministeredLocation = request.AdministeredLocation;
            vaccineRecord.AdministeredBy = request.AdministeredBy;

            await _VaccineRecordRepository.Update(vaccineRecord);
            var response = _mapper.Map<VaccineRecordResponseDto>(vaccineRecord);
            return response;
        }


    }
}