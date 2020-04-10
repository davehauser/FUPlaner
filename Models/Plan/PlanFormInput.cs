using System;
using System.Collections.Generic;
using System.Linq;
using FUPlaner.Entities;

namespace FUPlaner.Models
{
  // public class PlanFormInput {
  //   public int Id { get; set; }
  //   public IList<IList<string>> Tokens { get; set; }
  // }
  public class PlanFormInput
  {
    public int Id { get; set; }
    public int Level { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public IList<Day> Days { get; set; } = new List<Day>();

    public Plan ToEntity()
    {
      var plan = new Plan
      {
        Start = Start,
        End = End,
        Days = Days.Select(x => new Plan.Day
        {
          LessonTokens = x.Tokens.Select(t => new LessonToken(t)).ToList(),
        }).ToList()
      };
      return plan;
    }
    public Plan UpdateEntity(Plan plan)
    {
      plan.Days = Days.Select(x => new Plan.Day
      {
        LessonTokens = x.Tokens.Where(x => x != null).Select(t => new LessonToken(t)).ToList()
      }).ToList();
      plan.Start = Start;
      plan.End = End;
      return plan;
    }

    public class Day
    {
      public IList<string> Tokens { get; set; }
    }
  }
}