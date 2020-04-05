using System;
using System.ComponentModel.DataAnnotations;
using FUPlaner.Entities;
using FUPlaner.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace FUPlaner.Models
{
    public class LessonBase
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Fach-Kürzel")]
        [Required]
        public string SubjectToken { get; set; }

        [Display(Name = "Fach")]
        public string SubjectName { get; set; }

        [Display(Name = "Klasse")]
        public int Level { get; set; }

        [Display(Name = "Lektionsnummer")]
        public int LessonNumber { get; set; } = 1;

        [Display(Name = "Lektionskürzel")]
        public string Token
        {
            get
            {
                return SubjectToken + (Level > 0 ? $"{Level}." : "") + $"{LessonNumber:00}";
            }
            set
            {
              SubjectToken = value.GetSubjectTokenFromLessonToken();
              Enum.TryParse(SubjectToken, out Subject subject);
              SubjectName = subject.GetDescription();
              Level = value.GetLevelFromLessonToken();
              LessonNumber = value.GetLessonNumberromLessonToken();
            }
        }

        public bool IsAppointment => AppointmentTime.IsNotNullOrEmpty();

        [Display(Name = "Fixpunkt")]
        [DataType(DataType.Time)]
        public string AppointmentTime { get; set; }

        [Display(Name = "Abgabe / Meldung ")]
        public bool MustSend { get; set; }

        [Display(Name = "Auftrag ")]
        [DataType(DataType.MultilineText)]
        public string Task { get; set; }

        public class Link
        {
            [Display(Name = "Url")]
            public string Url { get; set; }

            [Display(Name = "Anzeigetext")]
            public string DisplayText { get; set; }
            public bool HasDisplayText => DisplayText.IsNotNullOrEmpty();
        }

    }
}