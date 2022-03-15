using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using MeetingDateProposer.Models.AccountApiModels;
using MeetingDateProposer.Models.ApplicationApiModels;

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
        private readonly IMapper _mapper;

        public CalendarController(
            ICalendarProvider calendar,
            IUserService userService,
            IMapper mapper)
        {
            _calendar = calendar;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserApiModel>> AddCalendarToUserAsync(
            string authorizationCode,
            Guid userId)
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
            var userViewModel = _mapper.Map<ApplicationUserApiModel>(user);
            return Ok(userViewModel);
        }
    }
}
