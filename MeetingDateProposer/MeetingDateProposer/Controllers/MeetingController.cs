using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.DatabaseServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    [Authorize(Policy = "RequireAdminRole")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingProvider;

        public MeetingController(IMeetingService meetingProvider)
        {
            _meetingProvider = meetingProvider;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<Meeting> GetMeetingById(Guid meetingId)
        {
            var meeting = _meetingProvider.GetMeetingByIdFromDb(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }


        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public ActionResult<User> CreateMeeting(string name)
        {
            Meeting meeting = new Meeting {Name = name};
            _meetingProvider.AddMeetingToDb(meeting);
            return CreatedAtAction(nameof(GetMeetingById), new {meetingId = meeting.Id}, meeting);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> DeleteMeeting(Guid meetingId)
        {
            var meeting = _meetingProvider.DeleteMeeting(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);

        }
    }
}
