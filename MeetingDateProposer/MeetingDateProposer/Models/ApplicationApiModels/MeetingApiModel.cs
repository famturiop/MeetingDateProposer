using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeetingDateProposer.Models.ApplicationApiModels
{
    public class MeetingApiModel
    {
        public string Id { get; set; }

        public List<ApplicationUserApiModel> ConnectedUsers { get; set; }

        [Required]
        [MaxLength(ValidationRules.MeetingNameMaxLength)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
    }
}