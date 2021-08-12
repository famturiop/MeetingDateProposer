using System;
using MeetingDateProposer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.Providers;
using Microsoft.AspNetCore.Http;

namespace MeetingDateProposer.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class DbController : ControllerBase
    {
        private readonly IUserProvider _userProvider;
        private readonly ICalendarProvider _userCalendar;

        public DbController(IUserProvider userProvider, ICalendarProvider userCalendar)
        {
            _userProvider = userProvider;
            _userCalendar = userCalendar;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userProvider.GetUserbyIdFromDb(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Calendar> GetCalendarById(int id)
        {
            var calendar = _userProvider.GetCalendarbyIdFromDb(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return calendar;
        }

        [HttpGet("{meetingId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Meeting> MyMeeting(int meetingId)
        {
            var meeting = _userProvider.GetMeetingbyIdFromDb(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            return meeting;
        }

        [HttpPut("{meetingId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Meeting> UpdateMeeting([FromRoute]int meetingId, [FromBody]int userId)
        {
            var meeting = _userProvider.GetMeetingbyIdFromDb(meetingId);
            var user = _userProvider.GetUserbyIdFromDb(userId);
            if (meeting == null) { return NotFound(); }
            if (user == null) { return NotFound(); }
            _userProvider.AddUserToMeeting(user,meeting);
            return Ok(meeting);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<User> CreateMeeting(Meeting meeting)
        {
            _userProvider.AddMeetingToDb(meeting);
            // generate unique meeting key that isn't obvious
            return CreatedAtAction(nameof(MyMeeting),new {id = meeting.Id}, meeting);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> CreateUser(User user)
        {
            _userCalendar.GetCalendar(user);
            _userProvider.AddUserToDb(user);
            return Ok(user);
        }


    }
}
