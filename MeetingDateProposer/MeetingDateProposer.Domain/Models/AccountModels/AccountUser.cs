using System;
using Microsoft.AspNetCore.Identity;

namespace MeetingDateProposer.Domain.Models.AccountViewModels
{
    public class AccountUser : IdentityUser<Guid>
    {
    }
}