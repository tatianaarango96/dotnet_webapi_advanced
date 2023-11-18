﻿using AutoMapper;
using MyVaccine.WebApi.Configurations.Validators;
using MyVaccine.WebApi.Dtos.Allergy;
using MyVaccine.WebApi.Dtos.Dependent;
using MyVaccine.WebApi.Models;

namespace MyVaccine.WebApi.Configurations.AutoMapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Dependent, DependentRequestDto>().ReverseMap();
        CreateMap<Dependent, DependentResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DependentId)).ReverseMap();

        CreateMap<Allergy, AllergyResponseDto>().ReverseMap();
        CreateMap<Allergy, AllergyResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AllergyId)).ReverseMap();



    }
}

