﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer;
using MeetingDateProposer.BusinessLayer.DatabaseServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    public class InteractionController : ControllerBase
    {
        private readonly IUserService _userProvider;
        private readonly IMeetingService _meetingProvider;
        private readonly ICalendarProvider _userCalendar;
        private readonly ICalendarCalculator _calculator;

        public InteractionController(IUserService userProvider, IMeetingService meetingProvider, 
            ICalendarProvider userCalendar, ICalendarCalculator calculator)
        {
            _userProvider = userProvider;
            _meetingProvider = meetingProvider;
            _userCalendar = userCalendar;
            _calculator = calculator;
        }

        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Meeting> UpdateMeeting(Guid meetingId, Guid userId)
        {
            var meeting = _meetingProvider.GetMeetingbyIdFromDb(meetingId);
            var user = _userProvider.GetUserbyIdFromDb(userId);
            if (meeting == null) { return NotFound(); }
            if (user == null) { return NotFound(); }
            _meetingProvider.AddUserToMeeting(user, meeting);
            return Ok(meeting);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Calendar> CalculateMeetingTime(Guid meetingId)
        {
            var meeting = _meetingProvider.GetMeetingbyIdFromDb(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            var calendar = _calculator.CalculateAvailableMeetingTime(meeting);
            return Ok(calendar);
        }
    }
}