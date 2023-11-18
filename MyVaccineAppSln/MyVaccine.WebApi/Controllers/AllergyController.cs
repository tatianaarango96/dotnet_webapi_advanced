using System.Security.AccessControl;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Models;
using MyVaccine.WebApi.Services.Contracts;

namespace MyVaccine.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AllergyController : ControllerBase
{
    private readonly IAllergyService _allergyService;
    private readonly IValidator<AllergyRequestDto> _validator;

    public AllergyController(IAllergyService allergyService, IValidator<AllergyRequestDto> validator)
    {
        _allergyService = allergyService;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var allergy = await _allergyService.GetAll();
        return Ok(allergy);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var allergy = await _allergyService.GetById(id);
        return Ok(allergy);
    }

    [HttpGet("get-Allergy-by-userid/{userId}")]
    public async Task<IActionResult> GetAllergyByUserId(int userId)
    {
        var allergy = await _allergyService.GetAllergyByUserId(userId);
        return Ok(allergy);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AllergyRequestDto allergyDto)
    {
        var validationResult = await _validator.ValidateAsync(allergyDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var allergy = await _allergyService.Add(allergyDto);
        return Ok(allergy);
    }

    //    var dependent = _mapper.Map<Dependent>(dependentsDto);
    //    await _dependentRepository.Add(dependent);

    //    return CreatedAtAction(nameof(GetById), new { id = dependent.Id }, dependent);
    //}

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AllergyRequestDto allergyDto)
    {

        var allergy = await _allergyService.Update(allergyDto, id);
        if (allergy == null)
        {
            return NotFound();
        }

        return Ok(allergy);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var allergy = await _allergyService.Delete(id);
        if (allergy == null)
        {
            return NotFound();
        }

        return Ok(allergy);
    }

}