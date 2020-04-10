using System.Collections.Generic;

namespace FUPlaner.Models
{
  public class LessonFormInput : LessonBase
  {
    public IList<Link> Links { get; set; }
  }
}