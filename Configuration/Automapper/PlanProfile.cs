using AutoMapper;
using FUPlaner.Entities;
using FUPlaner.Models;

namespace FUPlaner.Configuration
{
    public class LPlanProfile : Profile
    {
        public void PlanProfile()
        {
            CreateMap<Plan, PlanDisplay>()
              .ForMember(
                dest => dest.Start,
                opt => opt.MapFrom(src => src.Start.ToString("dd. MMM"))
              )
              .ForMember(
                dest => dest.End,
                opt => opt.MapFrom(src => src.End.ToString("dd. MMM"))
              );

            CreateMap<Plan.Day, PlanDisplay.Day>().ReverseMap();
        }
    }
}