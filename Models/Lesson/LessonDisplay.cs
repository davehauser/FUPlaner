using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUPlaner.Models
{
  public class LessonDisplay : LessonBase
  {
    public IList<Link> Links { get; set; } = new List<Link>();
    public IList<SelectListItem> LessonsList { get; set; } = new List<SelectListItem>();
  }
}