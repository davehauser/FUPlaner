using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FUPlaner.Data;
using FUPlaner.Entities;
using FUPlaner.Helpers;
using FUPlaner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FUPlaner.Controllers {
  public class LessonsController : Controller {
    private readonly IData _data;
    private readonly IMapper _mapper;

    public LessonsController (IData data, IMapper mapper) {
      _data = data;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Index () {
      return View ();
    }

    [HttpGet ("{controller}/{subject}")]
    public IActionResult Index (Subject subject) {
      var lessons = _data.Lessons.FindAll()
        .Where(x => x.Subject == subject)
        .OrderBy (x => x.LessonNumber);
      //Find (x => x.Subject == subject);
      var viewModel = new LessonDisplay (subject);
      viewModel.Lessons = _mapper.Map<List<LessonDisplay.Lesson>> (lessons);
      return View ("IndexBySubject", viewModel);
    }

    [HttpGet]
    public IActionResult Create (Subject subject) {
      var viewModel = new LessonFormDisplay {
        SubjectToken = subject.ToString (),
        SubjectName = subject.GetDescription ()
      };
      return View (viewModel);
    }

    [HttpPost]
    public IActionResult Create (LessonFormInput form) {
      if (!ModelState.IsValid) {
        TempData["messageType"] = "error";
        TempData["messageText"] = "Bitte vervollständige die Eingaben.";
        var viewModel = _mapper.Map<LessonFormDisplay> (form);
        return View ("Create", viewModel);
      }

      var lesson = _mapper.Map<Lesson> (form);
      _data.Lessons.Save (lesson);
      TempData["messageType"] = "success";
      TempData["messageText"] = "Die Lektion wurde erfolgreich hinzugefügt.";
      return RedirectToAction ("Index", new { subject = form.SubjectToken });
    }

    [HttpGet]
    public IActionResult Edit (int id) {
      var lesson = _data.Lessons.FindById (id);
      if (lesson == null) {
        return RedirectToAction ("Index");
      }

      var viewModel = _mapper.Map<LessonFormDisplay> (lesson);
      return View (viewModel);
    }

    [HttpPost]
    public IActionResult Edit (LessonFormInput form) {
      if (!ModelState.IsValid) {
        TempData["messageType"] = "error";
        TempData["messageText"] = "Bitte vervollständig die Eingaben.";
        var viewModel = _mapper.Map<LessonFormDisplay> (form);
        return View ("Edit", viewModel);
      }

      var lesson = _data.Lessons.FindById (form.Id);
      _mapper.Map (form, lesson);
      _data.Lessons.Save (lesson);
      TempData["messageType"] = "success";
      TempData["messageText"] = "Die Lektion wurde erfolgreich geändert.";
      return RedirectToAction ("Index", new { subject = form.SubjectToken });
    }

  }
}