using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<ApplicationUserApiModel>> AddGoogleCalendarToUser(
            string authorizationCode,
            Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
                return NotFound();

            var calendar = await _calendar.GetCalendarAsync(authorizationCode, userId);

            await _userService.AddCalendarToUserAsync(user, calendar);
            var userViewModel = _mapper.Map<ApplicationUserApiModel>(user);

            return Ok(userViewModel);
        }
    }
}
