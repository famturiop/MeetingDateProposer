using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    public class UserMeetingInteractionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMeetingService _meetingService;
        private readonly ICalendarCalculator _calculator;

        public UserMeetingInteractionController(
            IUserService userService,
            IMeetingService meetingService,
            ICalendarCalculator calculator)
        {
            _userService = userService;
            _meetingService = meetingService;
            _calculator = calculator;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Meeting>> UpdateMeetingAsync(Guid meetingId, Guid userId)
        {
            var meeting = await _meetingService.GetMeetingByIdFromDbAsync(meetingId);
            var user = await _userService.GetUserByIdFromDbAsync(userId);
            if (meeting == null || user == null)
            {
                return NotFound();
            }
            await _meetingService.AddUserToMeetingAsync(user, meeting);
            return Ok(meeting);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Calendar>> CalculateMeetingTimeAsync(Guid meetingId)
        {
            var meeting = await _meetingService.GetMeetingByIdFromDbAsync(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            var calendar = _calculator.CalculateAvailableMeetingTime(meeting);
            return Ok(calendar);
        }
    }
}
