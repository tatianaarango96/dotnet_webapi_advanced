using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class FamilyGroupController : ControllerBase
    {
        private readonly IFamilyGroupService _familygroupService;
        private readonly IValidator<FamilyGroupRequestDto> _validator;


        public FamilyGroupController(IFamilyGroupService familygroupService, IValidator<FamilyGroupRequestDto> validator)
        {
            _familygroupService = familygroupService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var familygroup = await _familygroupService.GetAll();
            return Ok(familygroup);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var familygroup = await _familygroupService.GetById(id);
            return Ok(familygroup);
        }



        [HttpPost]
        public async Task<IActionResult> Create(FamilyGroupRequestDto familygroupDto)
        {
            var validationResult = await _validator.ValidateAsync(familygroupDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var familygroup = await _familygroupService.Add(familygroupDto);
            return Ok(familygroup);
        }

        //    var dependent = _mapper.Map<Dependent>(dependentsDto);
        //    await _dependentRepository.Add(dependent);

        //    return CreatedAtAction(nameof(GetById), new { id = dependent.Id }, dependent);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FamilyGroupRequestDto familygroupDto)
        {

            var familygroup = await _familygroupService.Update(familygroupDto, id);
            if (familygroup == null)
            {
                return NotFound();
            }

            return Ok(familygroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var familygroup = await _familygroupService.Delete(id);
            if (familygroup == null)
            {
                return NotFound();
            }

            return Ok(familygroup);
        }

    }

}