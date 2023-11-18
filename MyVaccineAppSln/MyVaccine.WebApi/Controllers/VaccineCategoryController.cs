using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;

namespace MyVaccine.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VaccineCategoryController : ControllerBase
    {
        private readonly IVaccineCategoryService _VaccineCategoryService;
        private readonly IValidator<VaccineCategoryRequestDto> _validator;

        public VaccineCategoryController(IVaccineCategoryService vaccineCategoryService, IValidator<VaccineCategoryRequestDto> validator)
        {
            _VaccineCategoryService = vaccineCategoryService;
            _validator = validator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccineCategory = await _VaccineCategoryService.GetAll();
            return Ok(vaccineCategory);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaccineCategory = await _VaccineCategoryService.GetById(id);
            return Ok(vaccineCategory);
        }



        [HttpPost]
        public async Task<IActionResult> Create(VaccineCategoryRequestDto vaccineCategoryDto)
        {
            var validationResult = await _validator.ValidateAsync(vaccineCategoryDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var vaccineCategory = await _VaccineCategoryService.Add(vaccineCategoryDto);
            return Ok(vaccineCategory);
        }

        //    var dependent = _mapper.Map<Dependent>(dependentsDto);
        //    await _dependentRepository.Add(dependent);

        //    return CreatedAtAction(nameof(GetById), new { id = dependent.Id }, dependent);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VaccineCategoryRequestDto vaccineCategoryDto)
        {

            var vaccineCategory = await _VaccineCategoryService.Update(vaccineCategoryDto, id);
            if (vaccineCategory == null)
            {
                return NotFound();
            }

            return Ok(vaccineCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vaccineCategory = await _VaccineCategoryService.Delete(id);
            if (vaccineCategory == null)
            {
                return NotFound();
            }

            return Ok(vaccineCategory);
        }



    }
}