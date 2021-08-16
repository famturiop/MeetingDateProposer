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

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
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
        public ActionResult<Meeting> GetMeetingById(Guid meetingId)
        {
            var meeting = _meetingProvider.GetMeetingbyIdFromDb(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }


        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<User> CreateMeeting(string name)
        {
            Meeting meeting = new Meeting { Name = name };
            _meetingProvider.AddMeetingToDb(meeting);
            return CreatedAtAction(nameof(GetMeetingById), new { meetingId = meeting.Id }, meeting);
        }

    }
}
