using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Domain.Models.ApplicationModels
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public List<ApplicationUser> ConnectedUsers { get; set; }
        [Required]
        [MaxLength(ValidationRules.MeetingNameMaxLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}