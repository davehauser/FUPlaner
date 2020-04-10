using System;
using System.Collections.Generic;

namespace FUPlaner.Entities
{
  public class Plan : Entity
  {
    public int Level { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public IList<Day> Days { get; set; } = new List<Day>();
    public class Day
    {
      public string Name { get; set; }
      public IList<LessonToken> LessonTokens { get; set; } = new List<LessonToken>();
    }
  }
}