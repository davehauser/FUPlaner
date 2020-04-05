using System.Collections.Generic;
using System.Linq;
using FUPlaner.Data;
using FUPlaner.Entities;
using FUPlaner.Helpers;

namespace FUPlaner.Models
{
    public class PlanDisplay
    {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Title => $"Wochenplan vom {Start} bis {End}";
        public IList<Day> Days { get; set; }

        public static PlanDisplay From(Plan plan, IData data)
        {
            return new PlanDisplay {
                Id = plan.Id,
                Start = plan.Start.ToString ("d. MMMM"),
                End = plan.End.ToString("d. MMMM"),
                Days = plan.Days.Select(x => new PlanDisplay.Day {
                    Date = x.Date.ToString("dddd, d.M."),
                    Name = x.Name,
                    LessonTokens = x.LessonTokens,
                    Lessons = data.Lessons.FindAll()
                                    .Where(l => x.LessonTokens.Contains(l.Token))
                                    .Select(l => new LessonDisplay {
                                        Token = l.Token,
                                        MustSend = l.MustSend,
                                        AppointmentTime = l.AppointmentTime,
                                        Task = l.Task
                                    }).ToList()
                }).ToList()
            };
        }
        public class Day
        {
            public string Date { get; set; }
            public string Name { get; set; }
            public IList<string> LessonTokens { get; set; }
            public IList<LessonDisplay> Lessons {get;set;}
        }
    }
}