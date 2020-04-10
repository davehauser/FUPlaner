using System;
using System.Collections.Generic;
using System.Linq;
using FUPlaner.Data;
using FUPlaner.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FUPlaner.Controllers
{
  public class ToolsController : Controller
  {
    private readonly IData _data;

    public ToolsController(IData data)
    {
      _data = data;
    }
    public IActionResult PopulateLessons(Subject subject, int level, int minLessonNumber = 1, int maxLessonNumber = 55)
    {
      var lessons = new List<Lesson>();
      for (var i = minLessonNumber; i <= maxLessonNumber; i++)
      {
        var lesson = new Lesson(subject, level, i);
        lessons.Add(lesson);
        _data.Lessons.Save(lesson);
      }
      return Json(lessons);
    }
    public IActionResult DeleteAllLessons()
    {
      for (int i = 1; i <= 10000; i++)
      {
        _data.Lessons.Delete(i);
      }
      return Json(true);
    }

    public IActionResult GenerateDemoPlan()
    {
      var start = new DateTime(2020, 04, 27);
      var plan = new Plan
      {
        Start = start,
        End = new DateTime(2020, 5, 1),
        Days = Globals.Plan.DefaultPlan.Select((x, index) => new Plan.Day
        {
          LessonTokens = x.Select(t => new LessonToken(t, 4, index)).ToList()
        }).ToList()
      };
      _data.Plans.Save(plan);
      return RedirectToAction("Index", new { id = plan.Id });
    }

    public IActionResult GetPlans()
    {
      var plans = _data.Plans.FindAll();
      return Json(plans);
    }
    public IActionResult GetLessons()
    {
      var lessons = _data.Lessons.FindAll();
      return Json(lessons);
    }
  }
}