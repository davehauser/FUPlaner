using AutoMapper;
using FUPlaner.Entities;
using FUPlaner.Models;

namespace FUPlaner.Configuration
{
  public class LessonProfile : Profile
  {
    public LessonProfile()
    {
      CreateMap<Lesson, LessonFormDisplay>()
        .ForMember(dest => dest.SubjectToken, opt => opt.Ignore())
        .ForMember(dest => dest.Level, opt => opt.Ignore())
        .ForMember(dest => dest.LessonNumber, opt => opt.Ignore())
        .ForMember(dest => dest.Token, opt => opt.MapFrom(x => x.Token.ToString()));

      CreateMap<LessonFormInput, LessonFormDisplay>();

      CreateMap<LessonFormInput, Lesson>()
        .ForMember(dest => dest.Token, opt => opt.MapFrom(src => new LessonToken(src.SubjectToken, src.Level, src.LessonNumber)));

      CreateMap<Lesson, LessonDisplay>()
        .ForMember(dest => dest.Token, opt => opt.MapFrom(x => x.Token.ToString()))
        .ForMember(dest => dest.SubjectToken, opt => opt.MapFrom(src => src.Token.Subject.ToString()));

      CreateMap<Lesson.Link, LessonBase.Link>().ReverseMap();
    }
  }
}