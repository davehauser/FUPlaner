namespace FUPlaner.Helpers {
  public static class StringHelpers {
    public static bool IsNullOrEmpty (this string s) {
      return string.IsNullOrEmpty (s);
    }

    public static bool IsNotNullOrEmpty (this string s) {
      return !s.IsNullOrEmpty ();
    }
  }
}