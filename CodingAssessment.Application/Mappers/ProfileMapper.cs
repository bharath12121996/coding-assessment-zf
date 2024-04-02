using AutoMapper;
using CodingAssessment.Application.Dtos;
using CodingAssessment.Domain.Entities;

namespace CodingAssessment.Application.Mappers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Result, ResultResponse>();

            CreateMap<Meta, MetaResponse>();
        }
    }
}
