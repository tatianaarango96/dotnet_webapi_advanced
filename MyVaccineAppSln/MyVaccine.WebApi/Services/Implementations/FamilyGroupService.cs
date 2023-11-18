using AutoMapper;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Repositories.Contracts;
using MyVaccine.WebApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MyVaccine.WebApi.Services.Implementations
{
    public class FamilyGroupService : IFamilyGroupService

    {

        private readonly IBaseRepository<FamilyGroup> _familygroupRepository;
        private readonly IMapper _mapper;
        public FamilyGroupService(IBaseRepository<FamilyGroup> FamilyGroupRepository, IMapper mapper)
        {
            _familygroupRepository = FamilyGroupRepository;
            _mapper = mapper;
        }
        public async Task<FamilyGroupResponseDto> Add(FamilyGroupRequestDto request)
        {
            var familygroup = new FamilyGroup();
            familygroup.Name = request.Name;


            await _familygroupRepository.Add(familygroup);
            var response = _mapper.Map<FamilyGroupResponseDto>(familygroup);
            return response;
        }

        public async Task<FamilyGroupResponseDto> Delete(int id)
        {
            var familygroup = await _familygroupRepository.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();

            await _familygroupRepository.Delete(familygroup);
            var response = _mapper.Map<FamilyGroupResponseDto>(familygroup);
            return response;
        }

        public async Task<IEnumerable<FamilyGroupResponseDto>> GetAll()
        {
            var familygroup = await _familygroupRepository.GetAll().AsNoTracking().ToListAsync();
            var response = _mapper.Map<IEnumerable<FamilyGroupResponseDto>>(familygroup);
            return response;
        }

        public async Task<FamilyGroupResponseDto> GetById(int id)
        {
            var familygroup = await _familygroupRepository.FindByAsNoTracking(x => x.FamilyGroupId == id).FirstOrDefaultAsync();
            var response = _mapper.Map<FamilyGroupResponseDto>(familygroup);
            return response;
        }

        public async Task<IEnumerable<FamilyGroupResponseDto>> GetFamilyGroupByUserId(int userId)
        {
            var familygroup = await _familygroupRepository.FindByAsNoTracking(x => x.FamilyGroupId == userId).ToListAsync();
            var response = _mapper.Map<IEnumerable<FamilyGroupResponseDto>>(familygroup);
            return response;
        }

        public async Task<FamilyGroupResponseDto> Update(FamilyGroupRequestDto request, int id)
        {
            var familygroup = await _familygroupRepository.FindBy(x => x.FamilyGroupId == id).FirstOrDefaultAsync();
            familygroup.Name = request.Name;


            await _familygroupRepository.Update(familygroup);
            var response = _mapper.Map<FamilyGroupResponseDto>(familygroup);
            return response;
        }
    }
}