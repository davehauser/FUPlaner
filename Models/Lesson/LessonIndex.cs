using System.Collections.Generic;
using FUPlaner.Entities;
using FUPlaner.Helpers;

namespace FUPlaner.Models
{
  public partial class LessonIndex : LessonBase
  {
    public LessonIndex() { }
    public LessonIndex(Subject subject)
    {
      Subject = subject.GetDescription();
      SubjectToken = subject.ToString();
    }
    public string Subject { get; set; }
    public List<LessonDisplay> Lessons { get; set; }
  }
}