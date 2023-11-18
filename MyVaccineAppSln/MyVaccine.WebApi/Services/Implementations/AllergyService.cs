using AutoMapper;
using Azure.Core;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class AllergyService : IAllergyService
    {
        private readonly IBaseRepository<Allergy> _allergyRepository;
        private readonly IMapper _mapper;

        public AllergyService(IBaseRepository<Allergy> allergytRepository, IMapper mapper)
        {
            _allergyRepository = allergytRepository;
            _mapper = mapper;
        }
        public async Task<AllergyResponseDto> Add(AllergyRequestDto request)
        {
            var allergy = new Allergy();
            allergy.Name = request.Name;
            allergy.UserId = request.UserId;

            await _allergyRepository.Add(allergy);
            var response = _mapper.Map<AllergyResponseDto>(allergy);
            return response;
        }

        public async Task<AllergyResponseDto> Delete(int id)
        {
            var allergy = await _allergyRepository.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();

            await _allergyRepository.Delete(allergy);
            var response = _mapper.Map<AllergyResponseDto>(allergy);
            return response;
        }

        public async Task<IEnumerable<AllergyResponseDto>> GetAll()
        {
            var allergy = await _allergyRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<AllergyResponseDto>>(allergy);
            return response;
        }

        public async Task<AllergyResponseDto> GetById(int id)
        {
            var allergy = await _allergyRepository.FindByAsNoTracking(x => x.AllergyId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<AllergyResponseDto>(allergy);
            return response;
        }
        public async Task<IEnumerable<AllergyResponseDto>> GetAllergyByUserId(int userId)
        {
            var allergy = await _allergyRepository.FindByAsNoTracking(x => x.UserId == userId).ToListAsync();
            var response = _mapper.Map<IEnumerable<AllergyResponseDto>>(allergy);
            return response;
        }

        public async Task<AllergyResponseDto> Update(AllergyRequestDto request, int id)
        {
            var allergy = await _allergyRepository.FindBy(x => x.AllergyId == id).FirstOrDefaultAsync();
            allergy.Name = request.Name;
            allergy.UserId = request.UserId;

            await _allergyRepository.Update(allergy);
            var response = _mapper.Map<AllergyResponseDto>(allergy);
            return response;
        }
    }


}