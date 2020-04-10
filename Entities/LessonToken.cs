using System;
using System.Linq;
using FUPlaner.Helpers;

namespace FUPlaner.Entities
{
  public class LessonToken
  {
    public LessonToken() { }
    public LessonToken(string token)
    {
      Subject = token.GetSubjectTokenFromLessonToken().ToEnum<Subject>();
      Level = token.GetLevelFromLessonToken();
      LessonNumber = token.GetLessonNumberromLessonToken();
    }
    public LessonToken(Subject subject, int level, int lessonNumber)
    {
      Construct(subject, level, lessonNumber);
    }
    public LessonToken(string subject, int level, int lessonnumber)
    {
      var isParsed = false;
      var parsedSubject = Subject.V;
      foreach (var s in Enum.GetValues(typeof(Subject)).Cast<Subject>())
      {
        if (s.GetDescription().Equals(subject, StringComparison.OrdinalIgnoreCase))
        {
          parsedSubject = s;
          isParsed = true;
          break;
        }
      }
      Construct(isParsed ? parsedSubject : subject.ToEnum<Subject>(), level, lessonnumber);
    }
    private void Construct(Subject subject, int level, int lessonNumber)
    {
      Subject = subject;
      Level = level;
      LessonNumber = lessonNumber;
    }

    public Subject Subject { get; set; }
    public int Level { get; set; }
    public int LessonNumber { get; set; }
    public new string ToString()
    {
      var output = Subject.ToString() + (Level > 0 ? $"{Level}." : ".") + $"{LessonNumber:00}";
      return output == "00" ?
        "" :
        output;
    }
  }
}