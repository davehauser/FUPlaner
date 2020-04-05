using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FUPlaner.Entities;
using FUPlaner.Helpers;
using FUPlaner.Models;

namespace FUPlaner.Configuration {
  public class LessonProfile : Profile {
    public LessonProfile () {
      CreateMap<Lesson, LessonFormDisplay> ()
        .ForMember(dest => dest.Token, opt => opt.Ignore())
        .ForMember (
          dest => dest.SubjectToken,
          opt => opt.MapFrom (x => x.Subject.ToString ()))
        .ForMember (
          dest => dest.SubjectName,
          opt => opt.MapFrom (src => src.Subject.GetDescription ())
        );

      CreateMap<LessonFormInput, LessonFormDisplay> ();

      CreateMap<LessonFormInput, Lesson> ()
        .ForMember(
          dest => dest.Subject,
          opt => opt.MapFrom(src => src.SubjectToken)
        );

      CreateMap<Lesson, LessonDisplay.Lesson> ()
        .ForMember (
          dest => dest.SubjectToken,
          opt => opt.MapFrom (src => src.Subject.ToString ()));

      CreateMap<Lesson.Link, LessonBase.Link> ().ReverseMap ();
    }
  }
}