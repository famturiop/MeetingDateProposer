using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Models.ApplicationApiModels
{
    public class ApplicationUserApiModel
    {
        public List<CalendarApiModel> Calendars { get; set; }

        public string Id { get; set; }

        [Required]
        [MaxLength(ValidationRules.ApplicationUserNameMaxLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}