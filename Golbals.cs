using System.Collections.Generic;
using FUPlaner.Entities;

namespace FUPlaner
{
  public static class Globals
  {
    public static IList<string> Days = new List<string> {
      "Montag",
      "Dienstag",
      "Mittwoch",
      "Donnerstag",
      "Freitag"
    };
    public static class Plan
    {
      public static IList<IList<Subject>> DefaultPlan => new List<IList<Subject>> {
        new List<Subject> { Subject.MA, Subject.D, Subject.NMG, Subject.TTG, Subject.F, Subject.P },
        new List<Subject> { Subject.MA, Subject.D, Subject.K, Subject.E, Subject.P, Subject.P },
        new List<Subject> { Subject.MA, Subject.D, Subject.NMG, Subject.TTG, Subject.F, Subject.P },
        new List<Subject> { Subject.MA, Subject.D, Subject.NMG, Subject.E, Subject.P, Subject.P },
        new List<Subject> { Subject.MA, Subject.D, Subject.NMG, Subject.TTG, Subject.P, Subject.P }
      };
    }
  }
}