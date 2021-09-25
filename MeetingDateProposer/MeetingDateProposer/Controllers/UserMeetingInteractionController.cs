using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Authorization;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    public class UserMeetingInteractionController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMeetingService _meetingService;
        private readonly ICalendarProvider _userCalendar;
        private readonly ICalendarCalculator _calculator;

        public UserMeetingInteractionController(IUserService userService, IMeetingService meetingService, 
            ICalendarProvider userCalendar, ICalendarCalculator calculator)
        {
            _userService = userService;
            _meetingService = meetingService;
            _userCalendar = userCalendar;
            _calculator = calculator;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Meeting> UpdateMeeting(Guid meetingId, Guid userId)
        {
            var meeting = _meetingService.GetMeetingByIdFromDb(meetingId);
            var user = _userService.GetUserByIdFromDb(userId);
            if (meeting == null) { return NotFound(); }
            if (user == null) { return NotFound(); }
            _meetingService.AddUserToMeeting(user, meeting);
            return Ok(meeting);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Calendar> CalculateMeetingTime(Guid meetingId)
        {
            var meeting = _meetingService.GetMeetingByIdFromDb(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            var calendar = _calculator.CalculateAvailableMeetingTime(meeting);
            return Ok(calendar);
        }
    }
}
