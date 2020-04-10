using System;
using System.Collections.Generic;
using System.Linq;
using FUPlaner.Entities;
using FUPlaner.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUPlaner.Models
{
  public class LessonFormDisplay : LessonBase
  {

    public IList<Link> Links { get; set; } = new List<Link>();

    public IList<SelectListItem> SubjectsList
    {
      get
      {
        var subjects = new List<SelectListItem> {
          new SelectListItem {
          Selected = (base.Level <= 0),
          Text = "-- Auswählen --",
          Value = ""
          }
        };
        foreach (Subject subject in Enum.GetValues(typeof(Subject)))
        {
          subjects.Add(new SelectListItem
          {
            Selected = (base.SubjectToken == subject.ToString()),
            Text = $"{subject.GetDescription()}",
            Value = $"{subject}"
          });
        }
        return subjects;
      }
    }
    public IList<SelectListItem> LevelsList
    {
      get
      {
        var levels = new List<SelectListItem> {
          new SelectListItem {
          Selected = (base.Level <= 0),
          Text = "Alle Klassen",
          Value = "0"
          }
        };
        foreach (var level in Enumerable.Range(4, 3))
        {
          levels.Add(new SelectListItem
          {
            Selected = (base.Level == level),
            Text = $"{level}",
            Value = $"{level}"
          });
        }
        return levels;
      }
    }
  }
}