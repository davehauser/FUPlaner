using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FUPlaner.Helpers {
  public static class EnumHelper {
    public static string GetDescription (this Enum e) {
      return e.GetType ()
        .GetMember (e.ToString ())
        .FirstOrDefault ()
        ?.GetCustomAttribute<DisplayAttribute> (false)
        ?.Name
        ?? e.ToString ();
    }
  }
}