using System;
using System.Collections.Generic;
using System.Linq;
using FUPlaner.Data;
using FUPlaner.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FUPlaner.Controllers {
  public class ToolsController : Controller {
    private readonly IData _data;

    public ToolsController (IData data) {
      _data = data;
    }
    public IActionResult PopulateLessons (Subject subject) {
      var lessons = new List<Lesson> ();
      for (var i = 1; i <= 100; i++) {
        var lesson = new Lesson (subject) {
          Level = 4,
          LessonNumber = i
        };
        lessons.Add (lesson);
        _data.Lessons.Save (lesson);
      }
      return Json (lessons);
    }
    public IActionResult DeleteAllLessons () {
      for (int i = 1; i <= 10000; i++) {
        _data.Lessons.Delete (i);
      }
      return Json (true);
    }

    public IActionResult GenerateDemoPlan()
    {
      var start = new DateTime(2020, 04, 27);
      var plan = new Plan {
        Start = start,
        End = new DateTime(2020, 5, 1),
        Days = Enumerable.Range(0, 5).Select(x => new Plan.Day {
          Date = start.AddDays(x),
          LessonTokens = new List<string> {
            "MA4.01", "D91", "NMG01", "TTG01"
          }
        }).ToList()
      };
      _data.Plans.Save(plan);
      return RedirectToAction("Index", new { id = plan.Id });
    }
  }
}