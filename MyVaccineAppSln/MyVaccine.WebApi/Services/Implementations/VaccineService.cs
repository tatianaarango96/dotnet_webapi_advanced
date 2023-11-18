using AutoMapper;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class VaccineService : IVaccineService
    {

        private readonly IBaseRepository<Vaccine> _VaccineRepository;
        private readonly IMapper _mapper;
        public VaccineService(IBaseRepository<Vaccine> vaccineRepository, IMapper mapper)
        {
            _VaccineRepository = vaccineRepository;
            _mapper = mapper;
        }

        public async Task<VaccineResponseDto> Add(VaccineRequestDto request)
        {
            var vaccine = new Vaccine();
            vaccine.Name = request.Name;
            vaccine.RequiresBooster = request.RequiresBooster;


            await _VaccineRepository.Add(vaccine);
            var response = _mapper.Map<VaccineResponseDto>(vaccine);
            return response;
        }

        public async Task<VaccineResponseDto> Delete(int id)
        {
            var vaccine = await _VaccineRepository.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();

            await _VaccineRepository.Delete(vaccine);
            var response = _mapper.Map<VaccineResponseDto>(vaccine);
            return response;
        }

        public async Task<IEnumerable<VaccineResponseDto>> GetAll()
        {
            var vaccine = await _VaccineRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineResponseDto>>(vaccine);
            return response;
        }

        public async Task<VaccineResponseDto> GetById(int id)
        {
            var vaccine = await _VaccineRepository.FindByAsNoTracking(x => x.VaccineId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<VaccineResponseDto>(vaccine);
            return response;
        }

        public async Task<VaccineResponseDto> Update(VaccineRequestDto request, int id)
        {
            var vaccine = await _VaccineRepository.FindBy(x => x.VaccineId == id).FirstOrDefaultAsync();
            vaccine.Name = request.Name;


            await _VaccineRepository.Update(vaccine);
            var response = _mapper.Map<VaccineResponseDto>(vaccine);
            return response;
        }
    }
}