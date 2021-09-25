using System;
using Microsoft.AspNetCore.Identity;

namespace MeetingDateProposer.Domain.Models.AccountModels
{
    public class AccountUser : IdentityUser<Guid>
    {
    }
}