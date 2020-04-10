using System.Text.RegularExpressions;

namespace FUPlaner.Helpers
{
  public static class StringHelpers
  {
    public static bool IsNullOrEmpty(this string s)
    {
      return string.IsNullOrEmpty(s);
    }

    public static bool IsNotNullOrEmpty(this string s)
    {
      return !s.IsNullOrEmpty();
    }

    public static string GetSubjectTokenFromLessonToken(this string lessonToken)
    {
      if (lessonToken == null)
      {
        return "";
      }
      return Regex.Match(lessonToken, "[A-Za-z]+").Value;
    }
    public static int GetLevelFromLessonToken(this string lessonToken)
    {
      if (lessonToken == null)
      {
        return 0;
      }
      var stringValue = Regex.Match(lessonToken, @"(\d)\.")?.Groups[1].Value;
      int.TryParse(stringValue, out var value);
      return value;
    }
    public static int GetLessonNumberromLessonToken(this string lessonToken)
    {
      if (lessonToken == null)
      {
        return 0;
      }
      var stringValue = Regex.Match(lessonToken, @"\d{2,}")?.Groups[0].Value;
      int.TryParse(stringValue, out var value);
      return value;
    }
  }
}