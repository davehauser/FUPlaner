using System.Collections.Generic;
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
  }
}