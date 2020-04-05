using System;
using System.Collections.Generic;
using FUPlaner.Entities;
using FUPlaner.Helpers;

namespace FUPlaner.Models {
  public class LessonDisplay : LessonBase {
    public LessonDisplay (Subject subject) {
      Subject = subject.GetDescription ();
      SubjectToken = subject.ToString ();
    }
    public string Subject { get; set; }
    public List<Lesson> Lessons { get; set; }
    public class Lesson : LessonBase {
      public IList<Link> Links { get; set; }
    }
  }
}