using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.FamilyGroup;
using MyVaccine.WebApi.Dtos.Vaccine;
using MyVaccine.WebApi.Dtos.VaccineCategory;
using MyVaccine.WebApi.Dtos.VaccineRecord;
using MyVaccine.WebApi.Services.Contracts;
using MyVaccine.WebApi.Services.Implementations;

namespace MyVaccine.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VaccineRecordController : ControllerBase
    {
        private readonly IVaccineRecordService _VaccineRecordService;
        private readonly IValidator<VaccineRecordRequestDto> _validator;

        public VaccineRecordController(IVaccineRecordService vaccineRecordService, IValidator<VaccineRecordRequestDto> validator)
        {
            _VaccineRecordService = vaccineRecordService;
            _validator = validator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vaccineRecord = await _VaccineRecordService.GetAll();
            return Ok(vaccineRecord);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaccineRecord = await _VaccineRecordService.GetById(id);
            return Ok(vaccineRecord);
        }

        [HttpGet("get-VaccineRecord-by-userid/{userId}")]
        public async Task<IActionResult> GetVaccineRecordByUserId(int userId)
        {
            var vaccineRecord = await _VaccineRecordService.GetVaccineRecordByUserId(userId);
            return Ok(vaccineRecord);
        }

        [HttpGet("get-VaccineRecord-by-dependetId/{dependetId}")]
        public async Task<IActionResult> GetVaccineRecordByDependentId(int dependetId)
        {
            var vaccineRecord = await _VaccineRecordService.GetVaccineRecordByDependentId(dependetId);
            return Ok(vaccineRecord);
        }

        [HttpGet("get-VaccineRecord-by-vaccineId/{vaccineId}")]
        public async Task<IActionResult> GetVaccineRecordByVaccineId(int vaccineId)
        {
            var vaccineRecord = await _VaccineRecordService.GetVaccineRecordByVaccineId(vaccineId);
            return Ok(vaccineRecord);
        }


        [HttpPost]
        public async Task<IActionResult> Create(VaccineRecordRequestDto vaccineRecordDto)
        {
            var validationResult = await _validator.ValidateAsync(vaccineRecordDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var vaccineRecord = await _VaccineRecordService.Add(vaccineRecordDto);
            return Ok(vaccineRecord);
        }

        //    var dependent = _mapper.Map<Dependent>(dependentsDto);
        //    await _dependentRepository.Add(dependent);

        //    return CreatedAtAction(nameof(GetById), new { id = dependent.Id }, dependent);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VaccineRecordRequestDto vaccineRecordDto)
        {

            var vaccineRecord = await _VaccineRecordService.Update(vaccineRecordDto, id);
            if (vaccineRecord == null)
            {
                return NotFound();
            }

            return Ok(vaccineRecord);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vaccineRecord = await _VaccineRecordService.Delete(id);
            if (vaccineRecord == null)
            {
                return NotFound();
            }

            return Ok(vaccineRecord);
        }



    }
}