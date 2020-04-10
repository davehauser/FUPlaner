using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FUPlaner.Data;
using FUPlaner.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUPlaner.Models
{

  public class PlanFormDisplay
  {
    public PlanFormDisplay()
    {
      LevelsList = Enumerable.Range(4, 3).Select(x => new SelectListItem { Text = $"{x}", Value = $"{x}" }).ToList();
      LevelsList.Insert(0, new SelectListItem { Text = "Alle Klassen", Value = "0", Selected = true });
      Days = Globals.Days.Select(x => new Day
      {
        Name = x
      }).ToList();
    }

    public static PlanFormDisplay From(Plan plan, IData data, IMapper mapper)
    {
      var allLessonTokens = data.Lessons.FindAll().Select(x => x.Token.ToString()).OrderBy(x => x).ToList();
      var planFormDisplay = new PlanFormDisplay
      {
        Id = plan.Id,
        Level = plan.Level,
        Start = plan.Start.ToString("yyyy-MM-dd"),
        End = plan.End.ToString("yyyy-MM-dd"),
        Days = plan.Days.Select((x, index) =>
        {
          var lessons = data.Lessons.FindAll()
            .Where(l => (x.LessonTokens.Select(x => x.ToString()).Contains(l.Token.ToString())));

          var planFormDisplay = new PlanFormDisplay.Day
          {
            Index = index,
            Name = plan.Start.AddDays(index).ToString("dddd, d.M."),
            LessonTokens = x.LessonTokens.Select(x => x.ToString()).ToList(),
            Lessons = x.LessonTokens.Select(x =>
            {
              var lesson = (lessons.FirstOrDefault(l => l.Token.ToString().Equals(x.ToString())) ?? new Lesson(x.Subject, x.Level, x.LessonNumber));
              var lessonDisplay = mapper.Map<LessonDisplay>(lesson);
              lessonDisplay.LessonsList = allLessonTokens.Select(x => new SelectListItem
              {
                Text = x,
                Value = x,
                Selected = x == lesson.Token.ToString()
              }).ToList();
              lessonDisplay.LessonsList.Insert(0, new SelectListItem { Text = "--", Value = "" });
              return lessonDisplay;
            }).ToList()
          };
          var lessonCount = planFormDisplay.Lessons.Count();
          for (var i = lessonCount; i < Math.Max(6, lessonCount + 3); i++)
          {
            var lessonDisplay = new LessonDisplay();
            lessonDisplay.LessonsList = allLessonTokens.Select(x => new SelectListItem
            {
              Text = x,
              Value = x,
              Selected = false
            }).ToList();
            lessonDisplay.LessonsList.Insert(0, new SelectListItem { Text = "--", Value = "" });
            planFormDisplay.Lessons.Add(lessonDisplay);
          }
          return planFormDisplay;
        }).ToList()
      };
      return planFormDisplay;
    }
    public static PlanFormDisplay CreateWithDefaults(int level, DateTime start, IData data)
    {
      var nextDate = start;
      var previousPlan = data.Plans.FindAll().Where(x => x.Level == level).OrderByDescending(x => x.Start).FirstOrDefault();
      var previousTokens = previousPlan.Days.SelectMany(x => x.LessonTokens).Where(x => x.Level == level || x.Level == 0);
      var nextLessonNumbers = new Dictionary<Subject, int>();
      foreach (var subject in Enum.GetValues(typeof(Subject)).Cast<Subject>())
      {
        nextLessonNumbers.Add(subject, (previousTokens.Where(x => x.Subject == subject).OrderByDescending(x => x.LessonNumber).FirstOrDefault()?.LessonNumber ?? 0) + 1);
      }

      var plan = new PlanFormDisplay
      {
        Level = level,
        Start = start.ToString("dd.MM.yyyy"),
        End = start.AddDays(5).ToString("dd.MM.yyyy"),
        Days = Globals.Plan.DefaultPlan.Select(p =>
         new Day
         {
           LessonTokens = p.Take(level).Select(x =>
           {
             nextLessonNumbers.TryGetValue(x, out int nextLessonNumber);
             var lessonToken = new LessonToken(x, level, nextLessonNumber);
             nextLessonNumbers[x] = nextLessonNumber + 1;
             return lessonToken.ToString();
           }).ToList()
         }).ToList()
      };
      return plan;
    }
    public int Id { get; set; }
    public int Level { get; set; }
    public IList<SelectListItem> LevelsList { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public IList<Day> Days { get; set; } = Enumerable.Range(0, 5).Select(x => new Day()).ToList();
    public class Day
    {
      public int Index { get; set; }
      public string Name { get; set; }
      public IList<string> LessonTokens { get; set; } = Enumerable.Range(0, 6).Select(x => "").ToList();
      public IList<LessonDisplay> Lessons { get; set; } = Enumerable.Range(0, 6).Select(x => new LessonDisplay()).ToList();
    }
  }
}