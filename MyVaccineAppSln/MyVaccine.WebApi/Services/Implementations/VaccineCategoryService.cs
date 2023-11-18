using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using AutoMapper;
using MyVaccine.WebApi.Dtos.Vaccine;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class VaccineCategoryService : IVaccineCategoryService
    {
        private readonly IBaseRepository<VaccineCategory> _VaccineCategoryRepository;
        private readonly IMapper _mapper;
        public VaccineCategoryService(IBaseRepository<VaccineCategory> vaccinecategoryRepository, IMapper mapper)
        {
            _VaccineCategoryRepository = vaccinecategoryRepository;
            _mapper = mapper;
        }
        public async Task<VaccineCategoryResponseDto> Add(VaccineCategoryRequestDto request)
        {
            var vaccineCategory = new VaccineCategory();
            vaccineCategory.Name = request.Name;



            await _VaccineCategoryRepository.Add(vaccineCategory);
            var response = _mapper.Map<VaccineCategoryResponseDto>(vaccineCategory);
            return response;
        }

        public async Task<VaccineCategoryResponseDto> Delete(int id)
        {
            var vaccineCategory = await _VaccineCategoryRepository.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();

            await _VaccineCategoryRepository.Delete(vaccineCategory);
            var response = _mapper.Map<VaccineCategoryResponseDto>(vaccineCategory);
            return response;
        }

        public async Task<IEnumerable<VaccineCategoryResponseDto>> GetAll()
        {
            var vaccineCategory = await _VaccineCategoryRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<VaccineCategoryResponseDto>>(vaccineCategory);
            return response;
        }

        public async Task<VaccineCategoryResponseDto> GetById(int id)
        {
            var vaccineCategory = await _VaccineCategoryRepository.FindByAsNoTracking(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<VaccineCategoryResponseDto>(vaccineCategory);
            return response;
        }

        public async Task<VaccineCategoryResponseDto> Update(VaccineCategoryRequestDto request, int id)
        {
            var vaccineCategory = await _VaccineCategoryRepository.FindBy(x => x.VaccineCategoryId == id).FirstOrDefaultAsync();
            vaccineCategory.Name = request.Name;


            await _VaccineCategoryRepository.Update(vaccineCategory);
            var response = _mapper.Map<VaccineCategoryResponseDto>(vaccineCategory);
            return response;
        }
    }
}