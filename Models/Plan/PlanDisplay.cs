using System;
using System.Collections.Generic;
using System.Linq;
using FUPlaner.Data;
using FUPlaner.Entities;
using FUPlaner.Helpers;

namespace FUPlaner.Models {
    public class PlanDisplay {
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Title => $"Wochenplan vom {Start} bis {End}";
        public IList<Day> Days { get; set; }

        public static PlanDisplay From (Plan plan, IData data) {
            return new PlanDisplay {
                Id = plan.Id,
                    Start = plan.Start.ToString ("d. MMMM"),
                    End = plan.End.ToString ("d. MMMM"),
                    Days = plan.Days.Select ((x, index) => new PlanDisplay.Day {
                        Name = plan.Start.AddDays(index).ToString("dddd, d.M."),
                        LessonTokens = x.LessonTokens.Select (x => x.ToString ()).ToList (),
                            Lessons = data.Lessons.FindAll ()
                            .Where (l => (x.LessonTokens.Select (x => x.ToString ()).Contains (l.Token.ToString ())))
                            .Select (l => new LessonDisplay {
                                Token = l.Token.ToString (),
                                MustSend = l.MustSend,
                                AppointmentTime = l.AppointmentTime,
                                Task = l.Task,
                                Links = l.Links.Select (m => new LessonBase.Link { Url = m.Url, DisplayText = m.DisplayText }).ToList ()
                            }).ToList ()
                        }).ToList ()
            };
        }
        public class Day {
            public string Name { get; set; }
            public IList<string> LessonTokens { get; set; }
            public IList<LessonDisplay> Lessons { get; set; }
        }
    }
}