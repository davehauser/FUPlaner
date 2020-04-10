using System.ComponentModel.DataAnnotations;

namespace FUPlaner.Entities
{
  public enum Subject
  {
    [Display(Name = "Mathematik")]
    MA,
    [Display(Name = "Deutsch")]
    D,
    [Display(Name = "Natur, Mensch, Gesellschaft")]
    NMG,
    [Display(Name = "Englisch")]
    E,
    [Display(Name = "Gestalten")]
    TTG,
    [Display(Name = "Bewegung und Sport")]
    BS,
    [Display(Name = "Musik")]
    MU,
    [Display(Name = "Französisch")]
    F,
    [Display(Name = "Projekt")]
    P,
    [Display(Name = "Verschiedenes")]
    V,
    [Display(Name = "Klassenstunde")]
    K
  }
}