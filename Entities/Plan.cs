using System;
using System.Collections.Generic;
using FUPlaner.Helpers;

namespace FUPlaner.Entities
{
    public class Plan : Entity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public IList<Day> Days { get; set; }
        public class Day
        {
            public DateTime Date { get; set; }
            public string Name => Date.GetDayName() + $", {Date:d.M.}";
            public IList<string> LessonTokens { get; set; }
        }
    }
}