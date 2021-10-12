using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using MeetingDateProposer.BusinessLayer.DbInteractionServices;
using MeetingDateProposer.BusinessLayer.Providers;
using MeetingDateProposer.Domain.Models;
using MeetingDateProposer.Domain.Models.AccountModels;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using Microsoft.AspNetCore.Authorization;

namespace MeetingDateProposer.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    [Route("api/[action]")]
    [Authorize(Roles = Roles.Admin)]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public ActionResult<Meeting> GetMeetingById(Guid meetingId)
        {
            var meeting = _meetingService.GetMeetingByIdFromDb(meetingId);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }


        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [AllowAnonymous]
        public ActionResult<ApplicationUser> CreateMeeting(string name)
        {
            Meeting meeting = new Meeting {Name = name};
            _meetingService.AddMeetingToDb(meeting);
            return CreatedAtAction(nameof(GetMeetingById), new {meetingId = meeting.Id}, meeting);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ApplicationUser> DeleteMeeting(Guid meetingId)
        {
             _meetingService.DeleteMeeting(meetingId);
             return Ok();

        }
    }
}
