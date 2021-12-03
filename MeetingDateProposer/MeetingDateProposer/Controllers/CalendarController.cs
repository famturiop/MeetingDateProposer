using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using IdentityServer4.EntityFramework.Entities;
using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.Extensions.Configuration;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarProvider _calendar;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public CalendarController(ICalendarProvider calendar, IUserService userService, IConfiguration configuration)
        {
            _calendar = calendar;
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUser>> AddCalendarToUserAsync(string authorizationCode, Guid userId)
        {
            var user = await _userService.GetUserByIdFromDbAsync(userId);
            var calendar = await _calendar.GetCalendarAsync(authorizationCode, userId);

            if (user.Calendars == null)
            {
                user.Calendars = new List<Calendar> { calendar };
            }
            else
            {
                user.Calendars.Add(calendar);
            }

            await _userService.UpdateUserAsync(userId);

            return Ok(user);
        }
    }
}
