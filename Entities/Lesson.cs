using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FUPlaner.Helpers;

namespace FUPlaner.Entities
{
  public class Lesson : Entity
  {
    public Lesson() { }
    public Lesson(Subject subject, int level, int lessonNumber)
    {
      Token = new LessonToken(subject, level, lessonNumber);
    }
    public LessonToken Token { get; set; }
    public bool IsAppointment => AppointmentTime.IsNotNullOrEmpty();
    public string AppointmentTime { get; set; }
    public bool MustSend { get; set; }
    public string Task { get; set; }
    public LinkCollection Links { get; set; } = new LinkCollection();

    public class LinkCollection : Collection<Link>, IEnumerable<Link>, IEnumerable
    {

      System.Collections.IEnumerator IEnumerable.GetEnumerator()
      {
        foreach (var link in this.Items)
        {
          if (link.Url.IsNotNullOrEmpty())
          {
            yield return link;
          }
        }
      }
      new IEnumerator<Link> GetEnumerator()
      {
        foreach (var link in this.Items)
        {
          if (link.Url.IsNotNullOrEmpty())
          {
            yield return link;
          }
        }
      }
      protected override void InsertItem(int index, Link link)
      {
        if (link.Url.IsNotNullOrEmpty())
        {
          base.InsertItem(index, link);
        }
      }
      protected override void SetItem(int index, Link link)
      {
        if (link.Url.IsNotNullOrEmpty())
        {
          base.SetItem(index, link);
        }
      }
    }

    public class Link
    {
      public string Url { get; set; }
      public string DisplayText { get; set; }
    }
  }
}