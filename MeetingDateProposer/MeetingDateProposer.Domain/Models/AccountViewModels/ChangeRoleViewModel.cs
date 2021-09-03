using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MeetingDateProposer.Domain.Models.AccountViewModels
{
    public class ChangeRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}